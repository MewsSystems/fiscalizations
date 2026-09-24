namespace Mews.Fiscalizations.Hungary.Models;

public class Invoice
{
    private static readonly DateTime ZeroRateAcceptedFrom = new(2024, 1, 1);

    protected Invoice(
        InvoiceNumber number,
        InvoiceCategory category,
        DateTime issueDate,
        DateTime paymentDate,
        SupplierInfo supplierInfo,
        Receiver receiver,
        CurrencyCode currencyCode,
        ExchangeRate exchangeRate,
        ISequence<InvoiceItem> items,
        bool isSelfBilling,
        bool isCashAccounting,
        bool isCompleteDataReport,
        PaymentMethod? paymentMethod)
    {
        Number = number;
        Category = category;
        IssueDate = issueDate;
        PaymentDate = paymentDate;
        SupplierInfo = supplierInfo;
        Receiver = receiver;
        CurrencyCode = currencyCode;
        ExchangeRate = exchangeRate;
        Items = items;
        DeliveryDate = items.Values.Max(i => i.Value.DeliveryDate);
        TaxSummary = GetTaxSummary(items);
        IsSelfBilling = isSelfBilling;
        IsCashAccounting = isCashAccounting;
        IsCompleteDataReport = isCompleteDataReport;
        PaymentMethod = paymentMethod.ToOption();
    }

    public InvoiceNumber Number { get; }

    public InvoiceCategory Category { get; }

    /// <summary>The latest delivery date among the items, which is what NAV expects for an aggregate invoice.</summary>
    public DateTime DeliveryDate { get; }

    public DateTime IssueDate { get; }

    /// <summary>NAV paymentDate - the payment deadline, not the date the invoice was issued.</summary>
    public DateTime PaymentDate { get; }

    public Option<PaymentMethod> PaymentMethod { get; }

    public SupplierInfo SupplierInfo { get; }

    public Receiver Receiver { get; }

    public CurrencyCode CurrencyCode { get; }

    public ExchangeRate ExchangeRate { get; }

    public List<TaxSummaryItem> TaxSummary { get; }

    public ISequence<InvoiceItem> Items { get; }

    /// <summary>
    /// NAV selfBillingIndicator - true only when the customer issues the invoice in the supplier's name.
    /// </summary>
    public bool IsSelfBilling { get; }

    public bool IsCashAccounting { get; }

    /// <summary>
    /// NAV completenessIndicator. True declares that this data report *is* the electronic invoice
    /// (spec 2.6.2), which brings the electronic invoicing regime and its archiving duties with it, and is
    /// rejected outright for a private person or a company with no tax number. A caller that issues its own
    /// invoice document passes false.
    /// </summary>
    public bool IsCompleteDataReport { get; }

    public static Try<Invoice, Error> Create(
        InvoiceNumber number,
        InvoiceCategory category,
        DateTime issueDate,
        DateTime paymentDate,
        SupplierInfo supplierInfo,
        Receiver receiver,
        CurrencyCode currencyCode,
        ISequence<InvoiceItem> items,
        bool isSelfBilling = false,
        bool isCashAccounting = false,
        bool isCompleteDataReport = false,
        PaymentMethod? paymentMethod = null)
    {
        var checks = CheckLineExchangeRates(category, currencyCode, items)
            .FlatMap(_ => CheckZeroRateDeliveryDate(items))
            .FlatMap(_ => CheckReverseChargeCustomer(receiver, items))
            .FlatMap(_ => CheckCompleteness(isCompleteDataReport));
        return checks.FlatMap(_ =>
            GetExchangeRate(currencyCode, items).Map(rate => new Invoice(
                number,
                category,
                issueDate,
                paymentDate,
                supplierInfo,
                receiver,
                currencyCode,
                rate,
                items,
                isSelfBilling,
                isCashAccounting,
                isCompleteDataReport,
                paymentMethod
            ))
        );
    }

    /// <summary>
    /// Spec 2.2.2.4: the invoice exchange rate is the HUF net total divided by the net total in the invoice
    /// currency, and exactly 1 for a HUF invoice (NAV INCORRECT_HEAD_DATA_CURRENCY_CODE_HUF).
    /// </summary>
    private static Try<ExchangeRate, Error> GetExchangeRate(CurrencyCode currencyCode, ISequence<InvoiceItem> items)
    {
        if (currencyCode.Equals(TaxationInfo.DefaultCurrencyCode))
        {
            return ExchangeRate.Create(1m);
        }

        var net = items.Values.Sum(i => i.Value.TotalAmounts.Amount.Net.Value);
        var netHuf = items.Values.Sum(i => i.Value.TotalAmounts.AmountHUF.Net.Value);
        if (net != 0)
        {
            return ExchangeRate.Rounded(netHuf / net);
        }

        // A zero net total carries no rate of its own, so fall back to the gross totals, and to 1 when the
        // invoice is entirely zero valued - a correction that nets to zero still has to be reportable.
        var gross = items.Values.Sum(i => i.Value.TotalAmounts.Amount.Gross.Value);
        var grossHuf = items.Values.Sum(i => i.Value.TotalAmounts.AmountHUF.Gross.Value);
        return gross == 0 ? ExchangeRate.Create(1m) : ExchangeRate.Rounded(grossHuf / gross);
    }

    /// <summary>
    /// Spec 2.2.3.4: an aggregate invoice in a foreign currency reports the rate per item rather than once
    /// for the invoice, so every item has to carry one.
    /// </summary>
    private static Try<Unit, Error> CheckLineExchangeRates(InvoiceCategory category, CurrencyCode currencyCode, ISequence<InvoiceItem> items)
    {
        var isRequired = category == InvoiceCategory.Aggregate && !currencyCode.Equals(TaxationInfo.DefaultCurrencyCode);
        if (!isRequired || items.Values.All(i => i.Value.LineExchangeRate.NonEmpty))
        {
            return Try.Success<Unit, Error>(Unit.Value);
        }
        return Try.Error<Unit, Error>(new Error("An aggregate invoice in a foreign currency needs a line exchange rate on every item."));
    }

    /// <summary>
    /// Spec 3.3.2 item 14: a vatPercentage of 0 is accepted only for an invoice delivered on or after 2024-01-01.
    /// </summary>
    private static Try<Unit, Error> CheckZeroRateDeliveryDate(ISequence<InvoiceItem> items)
    {
        var hasZeroRate = items.Values.Any(i => i.Value.TotalAmounts.VatRate.Match(p => p == 0m, _ => false, _ => false, _ => false, _ => false));
        var deliveryDate = items.Values.Max(i => i.Value.DeliveryDate);
        if (!hasZeroRate || deliveryDate >= ZeroRateAcceptedFrom)
        {
            return Try.Success<Unit, Error>(Unit.Value);
        }
        return Try.Error<Unit, Error>(new Error("A 0% VAT rate is only accepted by NAV for an invoice delivered on or after 2024-01-01."));
    }

    /// <summary>
    /// Spec 3.3.2 item 50: a domestic reverse charge item needs the customer's Hungarian tax number.
    /// </summary>
    private static Try<Unit, Error> CheckReverseChargeCustomer(Receiver receiver, ISequence<InvoiceItem> items)
    {
        var hasReverseCharge = items.Values.Any(i => i.Value.TotalAmounts.VatRate.Match(_ => false, _ => false, _ => false, _ => true, _ => false));
        var isDomesticCompany = receiver.Match(customer => false, company => company.Match(local => true, foreign => false));
        if (!hasReverseCharge || isDomesticCompany)
        {
            return Try.Success<Unit, Error>(Unit.Value);
        }
        return Try.Error<Unit, Error>(new Error("A domestic reverse charge item needs a customer with a Hungarian tax number."));
    }

    /// <summary>
    /// Spec 3.3.2 item 40: NAV does not yet accept a data report as the electronic invoice itself, and rejects
    /// every completenessIndicator of true with INVOICE_COMPLETENESS_NOT_ALLOWED.
    /// </summary>
    private static Try<Unit, Error> CheckCompleteness(bool isCompleteDataReport)
    {
        if (!isCompleteDataReport)
        {
            return Try.Success<Unit, Error>(Unit.Value);
        }
        return Try.Error<Unit, Error>(new Error("NAV does not accept a data report as the electronic invoice yet."));
    }

    private static List<TaxSummaryItem> GetTaxSummary(ISequence<InvoiceItem> indexedItems)
    {
        return indexedItems.Values
            .GroupBy(i => i.Value.TotalAmounts.VatRate)
            .Select(g => new TaxSummaryItem(
                amount: Amount.Sum(g.Select(i => i.Value.TotalAmounts.Amount)),
                amountHUF: Amount.Sum(g.Select(i => i.Value.TotalAmounts.AmountHUF)),
                vatRate: g.Key
            ))
            .ToList();
    }
}
