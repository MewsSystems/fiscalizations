namespace Mews.Fiscalizations.Fiskaly.Models.DSFinVK.CashPointClosings;

public sealed record CashStatement(
    IEnumerable<BusinessCaseSummary> BusinessCases,
    CashStatementPayment Payment
);
