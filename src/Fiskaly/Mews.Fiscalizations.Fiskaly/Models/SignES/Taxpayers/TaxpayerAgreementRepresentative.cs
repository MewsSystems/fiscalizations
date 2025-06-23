namespace Mews.Fiscalizations.Fiskaly.Models.SignES.Taxpayers;

public record TaxpayerAgreementRepresentative(
    string FullName,
    string TaxNumber,
    string Municipality,
    string City,
    string Street,
    string PostalCode,
    string Number,
    string Country);