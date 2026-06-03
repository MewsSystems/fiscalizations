namespace Mews.Fiscalizations.Fiskaly.Models.DSFinVK.CashPointClosings;

public sealed record CashStatementPayment(
    decimal FullAmount,
    decimal CashAmount,
    IEnumerable<CurrencyAmount> CashAmountsByCurrency,
    IEnumerable<PaymentTypeAmount> PaymentTypes
);
