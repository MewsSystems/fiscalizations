namespace Mews.Fiscalizations.Fiskaly.Models.DSFinVK.CashPointClosings;

// All fields optional per spec; country_code is ISO 3166-1 alpha-3 (exactly 3 chars).
public sealed record BuyerAddress(
    string Street = null,
    string PostalCode = null,
    string City = null,
    string CountryCode = null
);
