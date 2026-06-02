namespace Mews.Fiscalizations.Fiskaly.Models.DSFinVK.CashPointClosings;

public sealed record CashPointClosing(
    Guid ClosingId,
    Guid ClientId,
    long CashPointClosingExportId,
    DateTimeOffset ExportCreationDate,
    DateOnly BusinessDate,
    IEnumerable<CashPointClosingTransaction> Transactions,
    CashStatement CashStatement
);
