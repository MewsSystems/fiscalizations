namespace Mews.Fiscalizations.Fiskaly.Models.DSFinVK.CashPointClosings;

// Buyer information (head.buyer). Name, BuyerExportId and Type are required by the spec.
public sealed record TransactionBuyer(
    string Name,
    string BuyerExportId,
    BuyerType Type,
    BuyerAddress Address = null,
    string VatIdNumber = null
);
