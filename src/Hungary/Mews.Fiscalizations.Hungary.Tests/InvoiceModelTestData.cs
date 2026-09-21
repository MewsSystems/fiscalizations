namespace Mews.Fiscalizations.Hungary.Tests;

/// <summary>
/// Builds valid Hungarian invoices so a test only has to state the one thing it is about. Every factory
/// funnels into <see cref="Build"/>, which supplies a plausible Hungarian supplier and a private person
/// receiver.
/// </summary>
internal static class InvoiceModelTestData
{
    internal static readonly DateTime DefaultDate = new(2026, 3, 1);

    public static Try<Invoice, Error> Create(string currency = "HUF", decimal net = 1000m, decimal netHuf = 1000m)
    {
        return Build(InvoiceCategory.Normal, currency, [Item(DefaultDate, net, netHuf, currency)]);
    }

    public static Try<Invoice, Error> Create(InvoiceCategory category)
    {
        return Build(category, "HUF", [Item(DefaultDate, 1000m, 1000m, "HUF")]);
    }

    public static Try<Invoice, Error> CreateAggregate(params DateTime[] deliveryDates)
    {
        return Build(InvoiceCategory.Aggregate, "HUF", [.. deliveryDates.Select(d => Item(d, 1000m, 1000m, "HUF"))]);
    }

    public static Try<Invoice, Error> CreateAggregateForeignCurrency()
    {
        return Build(InvoiceCategory.Aggregate, "EUR", [
            Item(DefaultDate, 100m, 39000m, "EUR"),
            Item(DefaultDate.AddDays(1), 50m, 19500m, "EUR")
        ]);
    }

    public static Try<Invoice, Error> CreateAggregateWithoutLineExchangeRate(string currency)
    {
        return Build(InvoiceCategory.Aggregate, currency, [Item(DefaultDate, 100m, 39000m, currency, withLineExchangeRate: false)]);
    }

    public static Try<Invoice, Error> CreateWithRates(params VatRate[] rates)
    {
        return Build(InvoiceCategory.Normal, "HUF", [.. rates.Select(r => Item(DefaultDate, 1000m, 1000m, "HUF", vatRate: r))]);
    }

    public static Try<Invoice, Error> CreateWithRate(VatRate rate)
    {
        return CreateWithRates(rate);
    }

    public static Try<Invoice, Error> CreateWithUnit(UnitOfMeasure unit)
    {
        return Build(InvoiceCategory.Normal, "HUF", [Item(DefaultDate, 1000m, 1000m, "HUF", unit: unit)]);
    }

    public static Try<Invoice, Error> CreateAdvance()
    {
        return Build(InvoiceCategory.Normal, "HUF", [Item(DefaultDate, 1000m, 1000m, "HUF", isAdvance: true)]);
    }

    public static Try<Invoice, Error> CreateForPrivatePerson()
    {
        return Create();
    }

    /// <param name="countryCode">
    /// Given explicitly when the number itself carries no country prefix, which is the shape that failed NAV
    /// schema validation in production.
    /// </param>
    public static Try<Invoice, Error> CreateForEuCompany(string communityVatNumber, string countryCode = null)
    {
        var country = Countries.GetByCode(countryCode ?? communityVatNumber[..2]).Get();
        var taxId = TaxpayerIdentificationNumber.Create(country, communityVatNumber).Success.Get();
        var receiver = Receiver.ForeignCompany(Models.Name.Create("Muster GmbH").Success.Get(), Address(country), taxId).Success.Get();
        return Build(InvoiceCategory.Normal, "HUF", [Item(DefaultDate, 1000m, 1000m, "HUF")], receiver);
    }

    public static Try<ModificationInvoice, Error> CreateStorno()
    {
        return Build(InvoiceCategory.Normal, "HUF", [Item(DefaultDate, -1000m, -1000m, "HUF")], number: "STORNO-1").FlatMap(i =>
            ModificationInvoice.Create(
                invoice: i,
                operation: ModificationOperation.Storno,
                originalDocumentNumber: InvoiceNumber.Create("ORIG-1").Success.Get(),
                modificationIndex: 1,
                itemIndexOffset: 1,
                modifyWithoutMaster: false
            )
        );
    }

    private static InvoiceItem Item(
        DateTime deliveryDate,
        decimal net,
        decimal netHuf,
        string currency,
        VatRate vatRate = null,
        UnitOfMeasure unit = null,
        bool isAdvance = false,
        bool withLineExchangeRate = true)
    {
        var rate = vatRate ?? VatRate.Percentage(0.27m).Success.Get();
        var amounts = new ItemAmounts(Amounts(net), Amounts(netHuf), rate);
        var isForeign = currency != "HUF";
        return new InvoiceItem(
            deliveryDate: deliveryDate,
            totalAmounts: amounts,
            unitAmounts: amounts,
            unitOfMeasure: unit ?? UnitOfMeasure.Piece(),
            description: Description.Create("Teszt tétel").Success.Get(),
            quantity: 1m,
            lineExchangeRate: isForeign && withLineExchangeRate ? ExchangeRate.Rounded(netHuf / net).Success.Get() : null,
            isAdvance: isAdvance
        );
    }

    private static Models.Amount Amounts(decimal net)
    {
        var tax = Math.Round(net * 0.27m, 2);
        return new Models.Amount(new AmountValue(net), new AmountValue(net + tax), new AmountValue(tax));
    }

    private static SimpleAddress Address(Country country)
    {
        return new SimpleAddress(
            city: City.Create("Budapest").Success.Get(),
            country: country,
            additionalAddressDetail: AdditionalAddressDetail.Create("Fő utca 1.").Success.Get(),
            postalCode: PostalCode.Create("1011").Success.Get()
        );
    }

    private static Try<Invoice, Error> Build(
        InvoiceCategory category,
        string currency,
        IReadOnlyList<InvoiceItem> items,
        Receiver receiver = null,
        string number = "TEST-1")
    {
        return Invoice.Create(
            number: InvoiceNumber.Create(number).Success.Get(),
            category: category,
            issueDate: DefaultDate,
            paymentDate: DefaultDate,
            supplierInfo: new SupplierInfo(
                taxpayerId: LocalTaxpayerIdentificationNumber.Create("12345678").Success.Get(),
                name: Models.Name.Create("Teszt Kft").Success.Get(),
                address: Address(Countries.Hungary),
                vatCode: VatCode.Create("2").Success.Get()
            ),
            receiver: receiver ?? Receiver.Customer(),
            currencyCode: CurrencyCode.Create(currency).Success.Get(),
            items: Sequence.FromPreordered(items, startIndex: 1).Get()
        );
    }
}
