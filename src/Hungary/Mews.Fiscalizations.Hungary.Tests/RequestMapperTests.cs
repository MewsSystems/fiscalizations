namespace Mews.Fiscalizations.Hungary.Tests;

public sealed class RequestMapperTests
{
    [Test]
    public void MapInvoice_NormalInvoice_OmitsAggregateLineData()
    {
        // aggregateInvoiceLineData is what switches NAV onto its aggregate-only per-line currency checks,
        // so a normal invoice must not carry it.
        var data = RequestMapper.MapInvoice(InvoiceModelTestData.Create(InvoiceCategory.Normal).Success.Get());

        Assert.That(GetFirstLine(data).aggregateInvoiceLineData, Is.Null);
    }

    [Test]
    public void MapInvoice_NormalInvoice_OmitsTheDeliveryPeriod()
    {
        var invoice = GetInvoice(RequestMapper.MapInvoice(InvoiceModelTestData.Create(InvoiceCategory.Normal).Success.Get()));

        Assert.That(invoice.invoiceHead.invoiceDetail.invoiceDeliveryPeriodStartSpecified, Is.False);
        Assert.That(invoice.invoiceHead.invoiceDetail.invoiceDeliveryPeriodEndSpecified, Is.False);
    }

    [Test]
    public void MapInvoice_NeverDeclaresAPeriodicSettlement()
    {
        // periodicalSettlement means a supply settled per period under Afa tv. 58., which no invoice this
        // library reports is. Leaving it unset while filling the period dates is NAV warning 561; setting it
        // to make the warning go away would misdeclare the tax point.
        var detail = GetInvoice(RequestMapper.MapInvoice(InvoiceModelTestData.Create(InvoiceCategory.Normal).Success.Get())).invoiceHead.invoiceDetail;

        Assert.That(detail.periodicalSettlementSpecified, Is.False);
        Assert.That(detail.periodicalSettlement, Is.False);
    }

    [Test]
    public void MapInvoice_AggregateInvoice_SetsLineDeliveryDateFromTheItem()
    {
        var deliveryDate = new DateTime(2026, 3, 2);
        var data = RequestMapper.MapInvoice(InvoiceModelTestData.CreateAggregate(deliveryDate, deliveryDate.AddDays(1)).Success.Get());

        Assert.That(GetFirstLine(data).aggregateInvoiceLineData.lineDeliveryDate, Is.EqualTo(deliveryDate));
    }

    [Test]
    public void MapInvoice_AggregateInvoice_OmitsTheDeliveryPeriodAndDatesTheInvoiceByTheLatestItem()
    {
        // A gyujtoszamla under Afa tv. 164. carries its dates per item; the header period belongs to a
        // periodic settlement under 58., which this is not. The header date is the latest item date.
        var data = RequestMapper.MapInvoice(InvoiceModelTestData.CreateAggregate(
            new DateTime(2026, 3, 1),
            new DateTime(2026, 3, 4)
        ).Success.Get());
        var detail = GetInvoice(data).invoiceHead.invoiceDetail;

        Assert.That(detail.invoiceDeliveryPeriodStartSpecified, Is.False);
        Assert.That(detail.invoiceDeliveryPeriodEndSpecified, Is.False);
        Assert.That(detail.periodicalSettlementSpecified, Is.False);
        Assert.That(detail.invoiceDeliveryDate, Is.EqualTo(new DateTime(2026, 3, 4)));
    }

    [Test]
    public void MapInvoice_AlwaysSetsLineExpressionIndicator()
    {
        // Mandatory since request version 1.1; leaving it false while sending quantity, unit and unit price
        // is what MANDATORY_LINE_CONTENT_MISSING is about.
        var data = RequestMapper.MapInvoice(InvoiceModelTestData.Create(InvoiceCategory.Normal).Success.Get());

        Assert.That(GetFirstLine(data).lineExpressionIndicator, Is.True);
    }

    [Test]
    public void MapInvoice_OwnUnitOfMeasure_SetsOwnValueAndOwnType()
    {
        var data = RequestMapper.MapInvoice(InvoiceModelTestData.CreateWithUnit(UnitOfMeasure.Own("Night").Success.Get()).Success.Get());
        var line = GetFirstLine(data);

        Assert.That(line.unitOfMeasure, Is.EqualTo(Dto.UnitOfMeasureType.OWN));
        Assert.That(line.unitOfMeasureSpecified, Is.True);
        Assert.That(line.unitOfMeasureOwn, Is.EqualTo("Night"));
    }

    [Test]
    public void MapInvoice_CanonicalUnitOfMeasure_OmitsOwnValue()
    {
        // unitOfMeasure used to serialize as its default member (PIECE) for every line because only the
        // Specified flag was set, so a night went out as a piece with an own value contradicting it.
        var data = RequestMapper.MapInvoice(InvoiceModelTestData.CreateWithUnit(UnitOfMeasure.Piece()).Success.Get());
        var line = GetFirstLine(data);

        Assert.That(line.unitOfMeasure, Is.EqualTo(Dto.UnitOfMeasureType.PIECE));
        Assert.That(line.unitOfMeasureOwn, Is.Null);
    }

    [Test]
    public void MapInvoice_OutOfScopeRate_EmitsVatOutOfScopeWithCaseAndReason()
    {
        var rate = VatRate.OutOfScope(TaxOutOfScopeCase.Atk, "Tourist tax");
        var data = RequestMapper.MapInvoice(InvoiceModelTestData.CreateWithRate(rate).Success.Get());
        var vatRate = ((Dto.LineAmountsNormalType)GetFirstLine(data).Item).lineVatRate;

        Assert.That(vatRate.ItemElementName, Is.EqualTo(Dto.ItemChoiceType2.vatOutOfScope));
        Assert.That(((Dto.DetailedReasonType)vatRate.Item).@case, Is.EqualTo("ATK"));
        Assert.That(((Dto.DetailedReasonType)vatRate.Item).reason, Is.EqualTo("Tourist tax"));
    }

    [Test]
    public void MapInvoice_ExemptRate_EmitsVatExemptionWithCaseAndReason()
    {
        var rate = VatRate.Exemption(TaxExemptionCase.Tam, "Tax exempt activity");
        var data = RequestMapper.MapInvoice(InvoiceModelTestData.CreateWithRate(rate).Success.Get());
        var vatRate = ((Dto.LineAmountsNormalType)GetFirstLine(data).Item).lineVatRate;

        Assert.That(vatRate.ItemElementName, Is.EqualTo(Dto.ItemChoiceType2.vatExemption));
        Assert.That(((Dto.DetailedReasonType)vatRate.Item).@case, Is.EqualTo("TAM"));
    }

    [Test]
    public void MapInvoice_PercentageRate_EmitsVatPercentage()
    {
        var data = RequestMapper.MapInvoice(InvoiceModelTestData.Create(InvoiceCategory.Normal).Success.Get());
        var vatRate = ((Dto.LineAmountsNormalType)GetFirstLine(data).Item).lineVatRate;

        Assert.That(vatRate.ItemElementName, Is.EqualTo(Dto.ItemChoiceType2.vatPercentage));
        Assert.That(vatRate.Item, Is.EqualTo(0.27m));
    }

    [Test]
    public void MapInvoice_AdvanceItem_SetsAdvanceIndicatorNotDepositIndicator()
    {
        // NAV depositIndicator means a bottle or container deposit, which Mews never reports; an advance
        // belongs in advanceData.
        var data = RequestMapper.MapInvoice(InvoiceModelTestData.CreateAdvance().Success.Get());
        var line = GetFirstLine(data);

        Assert.That(line.advanceData.advanceIndicator, Is.True);
        Assert.That(line.depositIndicatorSpecified, Is.False);
    }

    [Test]
    public void MapInvoice_OrdinaryItem_OmitsAdvanceData()
    {
        var data = RequestMapper.MapInvoice(InvoiceModelTestData.Create(InvoiceCategory.Normal).Success.Get());

        Assert.That(GetFirstLine(data).advanceData, Is.Null);
    }

    [Test]
    public void MapInvoice_CompletenessIndicatorIsWhatTheInvoiceDeclares()
    {
        // True declares that this data report is the electronic invoice (spec 2.6.2). Mews issues its own
        // PDF, so the mapping must not invent a true here the way it did for every company invoice.
        var data = RequestMapper.MapInvoice(InvoiceModelTestData.Create(InvoiceCategory.Normal).Success.Get());

        Assert.That(data.completenessIndicator, Is.False);
    }

    [Test]
    public void MapInvoice_SetsMergedItemIndicator()
    {
        Assert.That(GetInvoice(RequestMapper.MapInvoice(InvoiceModelTestData.Create(InvoiceCategory.Normal).Success.Get())).invoiceLines.mergedItemIndicator, Is.False);
    }

    [Test]
    public void MapInvoice_PrivatePerson_OmitsNameAddressAndVatData()
    {
        // NAV refuses a data report that carries customer details for a private person, whatever they say.
        var customerInfo = GetInvoice(RequestMapper.MapInvoice(InvoiceModelTestData.CreateForPrivatePerson().Success.Get())).invoiceHead.customerInfo;

        Assert.That(customerInfo.customerVatStatus, Is.EqualTo(Dto.CustomerVatStatusType.PRIVATE_PERSON));
        Assert.That(customerInfo.customerName, Is.Null);
        Assert.That(customerInfo.customerAddress, Is.Null);
        Assert.That(customerInfo.customerVatData, Is.Null);
    }

    [Test]
    public void MapInvoice_EuCompany_EmitsThePrefixedCommunityVatNumber()
    {
        // NAV's CommunityVatNumberType is [A-Z]{2}[0-9A-Z]{2,13}, so the country prefix has to be there.
        var customerInfo = GetInvoice(RequestMapper.MapInvoice(InvoiceModelTestData.CreateForEuCompany("DE811234567").Success.Get())).invoiceHead.customerInfo;

        Assert.That(customerInfo.customerVatStatus, Is.EqualTo(Dto.CustomerVatStatusType.OTHER));
        Assert.That(customerInfo.customerVatData.ItemElementName, Is.EqualTo(Dto.ItemChoiceType.communityVatNumber));
        Assert.That(customerInfo.customerVatData.Item, Is.EqualTo("DE811234567"));
    }

    [Test]
    public void MapModificationInvoice_Storno_ReferencesTheOriginalAndContinuesItsLineNumbering()
    {
        var data = RequestMapper.MapModificationInvoice(InvoiceModelTestData.CreateStorno().Success.Get());
        var invoice = GetInvoice(data);

        Assert.That(invoice.invoiceReference.originalInvoiceNumber, Is.EqualTo("ORIG-1"));
        Assert.That(invoice.invoiceReference.modificationIndex, Is.EqualTo(1));
        Assert.That(invoice.invoiceReference.modifyWithoutMaster, Is.False);
        // Spec 2.5.3: a modification's lines continue the original's numbering and always use CREATE.
        Assert.That(invoice.invoiceLines.line[0].lineModificationReference.lineNumberReference, Is.EqualTo("2"));
        Assert.That(invoice.invoiceLines.line[0].lineModificationReference.lineOperation, Is.EqualTo(Dto.LineOperationType.CREATE));
    }

    private static Dto.InvoiceType GetInvoice(Dto.InvoiceData data)
    {
        return (Dto.InvoiceType)data.invoiceMain.Items[0];
    }

    private static Dto.LineType GetFirstLine(Dto.InvoiceData data)
    {
        return GetInvoice(data).invoiceLines.line[0];
    }
}
