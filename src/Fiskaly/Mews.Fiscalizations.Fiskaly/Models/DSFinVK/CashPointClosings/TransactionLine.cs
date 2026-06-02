namespace Mews.Fiscalizations.Fiskaly.Models.DSFinVK.CashPointClosings;

public sealed record TransactionLine(
    string LineItemExportId,
    bool Storno,
    BusinessTransactionType BusinessTransactionType,
    IEnumerable<AmountPerVat> BusinessCaseAmountsPerVat,
    string ItemText,
    decimal Quantity,
    decimal PricePerUnit,
    decimal GrossAmount,
    decimal NetAmount
);
