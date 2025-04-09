namespace Mews.Fiscalizations.Sweden.Models;

public sealed class NewEnrollmentData(
    string chainName,
    string chainCode,
    string integrationVersion,
    string storeName,
    string storeId,
    string storeAddressLine,
    string storeZipCode,
    string storeCity,
    string storeCompanyOrgNr,
    string storeCompanyName,
    string partnerName,
    string partnerCode,
    string registerMake)
{
    public string ChainName { get; } = chainName;

    public string ChainCode { get; } = chainCode;

    public string IntegrationVersion { get; } = integrationVersion;

    public string StoreName { get; } = storeName;

    public string StoreId { get; } = storeId;

    public string StoreAddressLine { get; } = storeAddressLine;

    public string StoreZipCode { get; } = storeZipCode;

    public string StoreCity { get; } = storeCity;

    public string StoreCompanyOrgNr { get; } = storeCompanyOrgNr;

    public string StoreCompanyName { get; } = storeCompanyName;

    public string PartnerName { get; } = partnerName;

    public string PartnerCode { get; } = partnerCode;

    public string RegisterMake { get; } = registerMake;
}