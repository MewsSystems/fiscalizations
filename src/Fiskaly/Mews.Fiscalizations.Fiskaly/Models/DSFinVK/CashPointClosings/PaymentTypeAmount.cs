namespace Mews.Fiscalizations.Fiskaly.Models.DSFinVK.CashPointClosings;

// ForeignAmount/Name are optional; CurrencyCode is the cash register base currency (required).
public sealed record PaymentTypeAmount(
    PaymentType PaymentType,
    decimal Amount,
    string CurrencyCode,
    string Name = null,
    decimal? ForeignAmount = null
);
