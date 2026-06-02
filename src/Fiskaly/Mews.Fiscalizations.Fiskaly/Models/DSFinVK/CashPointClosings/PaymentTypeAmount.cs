namespace Mews.Fiscalizations.Fiskaly.Models.DSFinVK.CashPointClosings;

public sealed record PaymentTypeAmount(
    string PaymentType,
    decimal Amount,
    string CurrencyCode = null
);
