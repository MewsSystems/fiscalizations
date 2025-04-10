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

    /// <summary>
    /// The Swedish Tax Authority registered organization number for the company owning the POS.
    /// </summary>
    public string StoreCompanyOrgNr { get; } = storeCompanyOrgNr;

    public string StoreCompanyName { get; } = storeCompanyName;

    /// <summary>
    /// Partner name identifies the partner in the partner tree, such as “Acme AB”.
    /// </summary>
    public string PartnerName { get; } = partnerName;

    /// <summary>
    /// Is created by Infrasec as the unique namespace for the partner, such as “AC”.
    /// </summary>
    public string PartnerCode { get; } = partnerCode;

    /// <summary>
    /// The make of the register, as stated in the self assessment with the Tax Authority “Självdeklaration”.
    /// </summary>
    public string RegisterMake { get; } = registerMake;
}