namespace Mews.Fiscalizations.Fiskaly.Models.DSFinVK.CashPointClosings;

public sealed record CashPointClosingResult(
    Guid Id,
    Guid ClientId,
    long CashPointClosingExportId,
    CashPointClosingState State,
    string Error = null
);
