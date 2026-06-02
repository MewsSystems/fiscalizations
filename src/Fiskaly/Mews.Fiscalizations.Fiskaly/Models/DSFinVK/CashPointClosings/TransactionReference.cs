namespace Mews.Fiscalizations.Fiskaly.Models.DSFinVK.CashPointClosings;

// Fiskaly v1 only supports type "InterneTransaktion" for TSS-connected systems; the library maps it unconditionally.
public sealed record TransactionReference(
    Guid? TxId,
    Guid? ClosingId
);
