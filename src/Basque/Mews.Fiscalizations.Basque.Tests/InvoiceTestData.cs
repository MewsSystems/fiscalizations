namespace Mews.Fiscalizations.Basque.Tests;

internal static class InvoiceTestData
{
    internal static SendInvoiceRequest CreateCompleteInvoiceRequest(
        Issuer issuer,
        Software software,
        bool localReceivers,
        bool negativeInvoice,
        OriginalInvoiceInfo originalInvoiceInfo = null)
    {
        var taxSummary = CreateTaxSummary(negativeInvoice);
        var randomString = String1To20.CreateUnsafe(Guid.NewGuid().ToString()[..19]);
        return localReceivers.Match(
            t => SendInvoiceRequest.CreateCompleteLocalReceiverInvoiceRequest(
                issuer: issuer,
                invoiceFooter: new InvoiceFooter(software, originalInvoiceInfo: originalInvoiceInfo),
                receivers: CreateReceivers(localReceivers),
                invoiceData: CreateInvoiceData(negativeInvoice),
                taxSummary: taxSummary,
                number: randomString,
                issued: DateTime.Now,
                series: randomString,
                issuerType: IssuerType.IssuedByThirdParty
            ),
            f => SendInvoiceRequest.CreateCompleteForeignReceiverInvoiceRequest(
                issuer: issuer,
                invoiceFooter: new InvoiceFooter(software, originalInvoiceInfo: originalInvoiceInfo),
                receivers: CreateReceivers(localReceivers),
                invoiceData: CreateInvoiceData(negativeInvoice),
                taxBreakdown: OperationTypeTaxBreakdown.Create(delivery: taxSummary).Success.Get(),
                number: randomString,
                issued: DateTime.Now,
                series: randomString,
                issuerType: IssuerType.IssuedByThirdParty
            )
        );
    }

    internal static SendInvoiceRequest CreateSimplifiedInvoiceRequest(
        Issuer issuer,
        Software software,
        bool negativeInvoice,
        OriginalInvoiceInfo originalInvoiceInfo = null)
    {
        var randomString = String1To20.CreateUnsafe(Guid.NewGuid().ToString()[..19]);
        return SendInvoiceRequest.CreateSimplifiedInvoiceRequest(
            issuer: issuer,
            invoiceFooter: new InvoiceFooter(software, originalInvoiceInfo: originalInvoiceInfo),
            invoiceData: CreateInvoiceData(negativeInvoice),
            taxSummary: CreateTaxSummary(negativeInvoice),
            number: randomString,
            issued: DateTime.Now,
            series: randomString,
            issuerType: IssuerType.IssuedByThirdParty
        );
    }

    private static TaxSummary CreateTaxSummary(bool negativeInvoice)
    {
        var baseValue = negativeInvoice.Match(
            t => -73.86m,
            f => 73.86m
        );
        return TaxSummary.Create(taxed: CreateTaxRateSummary(21m, baseValue).ToEnumerable().ToArray()).Success.Get();
    }

    private static InvoiceData CreateInvoiceData(bool negativeInvoice)
    {
        return new InvoiceData(
            description: String1To250.CreateUnsafe("TicketBAI sample invoice test."),
            items: CreateInvoiceItems(negativeInvoice),
            totalAmount: negativeInvoice.Match(t => -89.36m, f => 89.36m),
            taxModes: TaxMode.GeneralTaxRegimeActivity.ToEnumerable(),
            transactionDate: DateTime.Now
        );
    }

    private static INonEmptyEnumerable<InvoiceItem> CreateInvoiceItems(bool negativeInvoice)
    {
        return negativeInvoice.Match(
            t => NonEmptyEnumerable.Create(
                new InvoiceItem(
                    description: String1To250.CreateUnsafe("Night 1"),
                    quantity: 1,
                    unitAmount: -23.356m,
                    discount: -2.00m,
                    totalAmount: -25.84m
                ),
                new InvoiceItem(
                    description: String1To250.CreateUnsafe("Night 2"),
                    quantity: 1.50m,
                    unitAmount: -18.2m,
                    totalAmount: -33.03m
                ),
                new InvoiceItem(
                    description: String1To250.CreateUnsafe("Parking"),
                    quantity: 18,
                    unitAmount: -1.40m,
                    totalAmount: -30.49m
                )
            ),
            f => NonEmptyEnumerable.Create(
                new InvoiceItem(
                    description: String1To250.CreateUnsafe("Night 1"),
                    quantity: 1,
                    unitAmount: 23.356m,
                    discount: 2.00m,
                    totalAmount: 25.84m
                ),
                new InvoiceItem(
                    description: String1To250.CreateUnsafe("Night 2"),
                    quantity: 1.50m,
                    unitAmount: 18.2m,
                    totalAmount: 33.03m
                ),
                new InvoiceItem(
                    description: String1To250.CreateUnsafe("Parking"),
                    quantity: 18,
                    unitAmount: 1.40m,
                    totalAmount: 30.49m
                )
            )
        );
    }

    private static INonEmptyEnumerable<Receiver> CreateReceivers(bool localReceivers)
    {
        return NonEmptyEnumerable.Create(localReceivers.Match(
            t => Receiver.Local(
                nif: "11111111H",
                name: Name.CreateUnsafe("Mike The Local"),
                postalCode: PostalCode.CreateUnsafe("08013"),
                address: String1To250.CreateUnsafe("C/ de Mallorca, 401, Barcelona")
            ).Success.Get(),
            f => Receiver.Foreign(
                idType: IdType.Passport,
                id: String1To20.CreateUnsafe("ABCDEF123"),
                name: Name.CreateUnsafe("John The Forienger"),
                postalCode: PostalCode.CreateUnsafe("12345678912345678BBA"),
                address: String1To250.CreateUnsafe("Prague, Italska 2502/555"),
                country: Countries.CzechRepublic
            )
        ));
    }

    private static TaxRateSummary CreateTaxRateSummary(decimal vat, decimal baseValue)
    {
        return new TaxRateSummary(
            taxRatePercentage: Percentage.Create(vat).Success.Get(),
            taxBaseAmount: Amount.Create(baseValue).Success.Get(),
            taxAmount: Amount.Create(Math.Round(baseValue * vat / 100, 2)).Success.Get()
        );
    }
}