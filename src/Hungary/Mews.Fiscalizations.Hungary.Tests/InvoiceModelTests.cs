namespace Mews.Fiscalizations.Hungary.Tests;

public sealed class InvoiceModelTests
{
    [Test]
    public void ExchangeRate_HufInvoice_IsExactlyOne()
    {
        // NAV INCORRECT_HEAD_DATA_CURRENCY_CODE_HUF: a HUF invoice must declare an exchange rate of exactly 1.
        var invoice = InvoiceModelTestData.Create(currency: "HUF", net: 1000m, netHuf: 1000m).Success.Get();

        Assert.That(invoice.ExchangeRate.Value, Is.EqualTo(1m));
    }

    [Test]
    public void ExchangeRate_ForeignCurrency_IsHufNetOverCurrencyNet()
    {
        // Spec 2.2.2.4: the invoice exchange rate is invoiceNetAmountHUF / invoiceNetAmount. It used to be
        // derived from the gross totals, which is a different number once rounding is involved.
        var invoice = InvoiceModelTestData.Create(currency: "EUR", net: 100m, netHuf: 39000m).Success.Get();

        Assert.That(invoice.ExchangeRate.Value, Is.EqualTo(390m));
    }

    [Test]
    public void Create_ZeroRateDeliveredFrom2024_Succeeds()
    {
        var rate = VatRate.Percentage(0m).Success.Get();

        Assert.That(InvoiceModelTestData.CreateWithRate(rate, new DateTime(2024, 1, 1)).IsSuccess, Is.True);
    }

    [Test]
    public void Create_ZeroRateDeliveredBefore2024_Fails()
    {
        // Spec 3.3.2 item 14: zero percent is accepted only for a delivery on or after 01/01/2024.
        var rate = VatRate.Percentage(0m).Success.Get();

        Assert.That(InvoiceModelTestData.CreateWithRate(rate, new DateTime(2023, 12, 31)).IsSuccess, Is.False);
    }

    [Test]
    public void Create_DomesticReverseChargeForAPrivatePerson_Fails()
    {
        // Spec 3.3.2 item 50: a domestic reverse charge needs the customer's Hungarian tax number.
        var result = InvoiceModelTestData.CreateWithRate(VatRate.DomesticReverseCharge(), InvoiceModelTestData.DefaultDate, Receiver.Customer());

        Assert.That(result.IsSuccess, Is.False);
    }

    [Test]
    public void Create_DomesticReverseChargeForADomesticCompany_Succeeds()
    {
        var result = InvoiceModelTestData.CreateWithRate(VatRate.DomesticReverseCharge(), InvoiceModelTestData.DefaultDate, InvoiceModelTestData.LocalCompany());

        Assert.That(result.IsSuccess, Is.True);
    }

    [Test]
    public void Create_CompleteDataReport_Fails()
    {
        // Spec 3.3.2 item 40: NAV rejects every completenessIndicator of true until e-invoicing is in force.
        Assert.That(InvoiceModelTestData.CreateCompleteDataReport().IsSuccess, Is.False);
    }

    [Test]
    public void DeliveryDate_IsTheLatestItemDeliveryDate()
    {
        // Spec 2.2.2.4: for an aggregate invoice the invoice delivery date is the latest line delivery date.
        var invoice = InvoiceModelTestData.CreateAggregate(
            new DateTime(2026, 3, 1),
            new DateTime(2026, 3, 4),
            new DateTime(2026, 3, 2)
        ).Success.Get();

        Assert.That(invoice.DeliveryDate, Is.EqualTo(new DateTime(2026, 3, 4)));
    }

    [Test]
    public void TaxSummary_GroupsByVatRateNotByPercentage()
    {
        // Two 27% lines collapse into one summary entry; an out of scope line stays separate even though it
        // also has no percentage. Grouping on a nullable percentage used to merge the latter with anything
        // else that lacked a rate.
        var invoice = InvoiceModelTestData.CreateWithRates(
            VatRate.Percentage(0.27m).Success.Get(),
            VatRate.Percentage(0.27m).Success.Get(),
            VatRate.OutOfScope(TaxOutOfScopeCase.Atk, "Tourist tax").Success.Get()
        ).Success.Get();

        Assert.That(invoice.TaxSummary.Count, Is.EqualTo(2));
    }

    [Test]
    public void Create_ForeignCurrencyAggregateWithoutLineRate_Fails()
    {
        // Spec 2.2.3.4: an aggregate invoice in a foreign currency reports the rate per item, so a missing
        // one is refused here rather than being sent as a silent 1.
        var result = InvoiceModelTestData.CreateAggregateWithoutLineExchangeRate(currency: "EUR");

        Assert.That(result.IsSuccess, Is.False);
    }

    [Test]
    public void Create_HufAggregateWithoutLineRate_Succeeds()
    {
        var result = InvoiceModelTestData.CreateAggregateWithoutLineExchangeRate(currency: "HUF");

        Assert.That(result.IsSuccess, Is.True);
    }

    [Test]
    public void ModificationInvoice_Create_SameNumberAsTheOriginal_Fails()
    {
        // NAV rejects a modification document whose own number equals the number it references.
        var invoice = InvoiceModelTestData.Create().Success.Get();

        var result = ModificationInvoice.Create(
            invoice: invoice,
            operation: ModificationOperation.Modify,
            originalDocumentNumber: invoice.Number,
            modificationIndex: 1,
            itemIndexOffset: 1,
            modifyWithoutMaster: false
        );

        Assert.That(result.IsSuccess, Is.False);
    }

    [Test]
    public void ModificationInvoice_Create_ModificationIndexBelowOne_Fails()
    {
        // The schema's InvoiceUnboundedIndexType starts at 1. The old code derived the index from a count of
        // earlier bills, which could legitimately be 0.
        var invoice = InvoiceModelTestData.Create().Success.Get();

        var result = ModificationInvoice.Create(
            invoice: invoice,
            operation: ModificationOperation.Modify,
            originalDocumentNumber: InvoiceNumber.Create("ORIG-1").Success.Get(),
            modificationIndex: 0,
            itemIndexOffset: 1,
            modifyWithoutMaster: false
        );

        Assert.That(result.IsSuccess, Is.False);
    }

    [Test]
    public void ModificationInvoice_Create_CarriesTheOperation()
    {
        var modification = InvoiceModelTestData.CreateStorno().Success.Get();

        Assert.That(modification.Operation, Is.EqualTo(ModificationOperation.Storno));
    }
}
