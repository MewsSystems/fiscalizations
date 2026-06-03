namespace Mews.Fiscalizations.Fiskaly.Models.DSFinVK.CashPointClosings;

public sealed record TransactionLine(
    string LineItemExportId,
    bool Storno,
    BusinessTransactionType BusinessTransactionType,
    IEnumerable<AmountPerVat> BusinessCaseAmountsPerVat,
    string ItemText,
    TransactionLineItem Item = null,
    string BusinessCaseName = null,
    Guid? PurchaserAgencyId = null,
    // in_house defaults to true on the Fiskaly side; leave null to omit.
    bool? InHouse = null,
    string VoucherId = null,
    Guid? SourceCashRegister = null,
    IEnumerable<TransactionReference> References = null
);

// DSFinV-K line `item` (optional). number/quantity/price_per_unit are required when item is present.
public sealed record TransactionLineItem(
    string Number,
    decimal Quantity,
    decimal PricePerUnit,
    string Gtin = null,
    decimal? QuantityFactor = null,
    string QuantityMeasure = null,
    string GroupId = null,
    string GroupName = null,
    IEnumerable<VatAmountBreakdown> BaseAmountsPerVat = null,
    IEnumerable<VatAmountBreakdown> DiscountsPerVat = null,
    IEnumerable<VatAmountBreakdown> ExtraAmountsPerVat = null,
    IEnumerable<TransactionSubItem> SubItems = null
);
