namespace Mews.Fiscalizations.Fiskaly.Models.DSFinVK.CashPointClosings;

public sealed record TransactionSecurity(
    Guid? TssTxId,
    string ErrorMessage = null
);
