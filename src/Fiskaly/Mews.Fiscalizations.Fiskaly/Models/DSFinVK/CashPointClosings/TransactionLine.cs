namespace Mews.Fiscalizations.Fiskaly.Models.DSFinVK.CashPointClosings;

public sealed record TransactionLine(
    string LineItemExportId,
    bool Storno,
    BusinessTransactionType BusinessTransactionType,
    IEnumerable<AmountPerVat> BusinessCaseAmountsPerVat,
    string ItemText,
    TransactionLineItem Item = null
);

// DSFinV-K line `item` (optional). number/quantity/price_per_unit are required when item is present.
public sealed record TransactionLineItem(
    string Number,
    decimal Quantity,
    decimal PricePerUnit
);
