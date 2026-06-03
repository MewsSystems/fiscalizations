namespace Mews.Fiscalizations.Fiskaly.Models.DSFinVK.CashPointClosings;

public sealed record BusinessCaseSummary(
    BusinessTransactionType Type,
    IEnumerable<AmountPerVat> AmountsPerVat
);
