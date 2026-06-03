namespace Mews.Fiscalizations.Fiskaly.Models.DSFinVK.CashPointClosings;

// Person who recorded the transaction at the cash register (head.user).
public sealed record TransactionUser(string UserExportId, string Name = null);
