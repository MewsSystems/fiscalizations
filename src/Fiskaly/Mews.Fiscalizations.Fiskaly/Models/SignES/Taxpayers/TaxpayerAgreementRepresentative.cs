namespace Mews.Fiscalizations.Fiskaly.Models.SignES.Taxpayers;

public record TaxpayerAgreementRepresentative(
    string FullName,
    string TaxNumber,
    TaxpayerAddress Address);