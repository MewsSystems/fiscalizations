namespace Mews.Fiscalizations.Fiskaly.Models.DSFinVK.CashPointClosings;

// Explains the composition of a sold product (e.g. a menu split into its parts).
// Number, Quantity and AmountPerVat are required when a sub-item is present.
public sealed record TransactionSubItem(
    string Number,
    decimal Quantity,
    VatAmountBreakdown AmountPerVat,
    string Gtin = null,
    string Name = null,
    decimal? QuantityFactor = null,
    string QuantityMeasure = null,
    string GroupId = null,
    string GroupName = null
);
