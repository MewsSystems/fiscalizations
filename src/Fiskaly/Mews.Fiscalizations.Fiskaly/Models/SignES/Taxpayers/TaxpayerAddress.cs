namespace Mews.Fiscalizations.Fiskaly.Models.SignES.Taxpayers;

public record TaxpayerAddress(string Municipality,
    string City,
    string Street,
    string PostalCode,
    string Number,
    string Country);