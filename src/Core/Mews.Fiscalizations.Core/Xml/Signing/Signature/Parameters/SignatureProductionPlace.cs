namespace Mews.Fiscalizations.Core.Xml.Signing.Signature.Parameters;

public sealed class SignatureProductionPlace(
    string city = null,
    string stateOrProvince = null,
    string postalCode = null,
    string countryName = null)
{
    public string City { get; } = city;

    public string StateOrProvince { get; } = stateOrProvince;

    public string PostalCode { get; } = postalCode;

    public string CountryName { get; } = countryName;
}