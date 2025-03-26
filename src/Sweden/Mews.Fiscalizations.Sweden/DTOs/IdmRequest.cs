using System.Xml.Serialization;

namespace Mews.Fiscalizations.Sweden.DTOs;

[XmlRoot("IdmRequest")]
public class IdmRequest
{
    [XmlElement("ApplicationID")]
    public required string ApplicationId { get; init; }

    [XmlElement("RequestID")]
    public required int RequestId { get; init; }

    [XmlElement("EnrollData")]
    public required EnrollData EnrollData { get; init; }
}

public sealed class EnrollData
{
    [XmlElement("Action")]
    public required string Action { get; init; }

    [XmlElement("PartnerAuthority")]
    public PartnerAuthority? PartnerAuthority { get; init; }

    [XmlElement("OrganizationBranch")]
    public OrganizationBranch? OrganizationBranch { get; init; }

    [XmlElement("OrganizationChain")]
    public OrganizationChain? OrganizationChain { get; init; }

    [XmlElement("StoreInfo")]
    public StoreInfo? StoreInfo { get; init; }

    [XmlElement("CompanyInfo")]
    public CompanyInfo? CompanyInfo { get; init; }

    [XmlElement("RegisterInfo")]
    public required RegisterInfo RegisterInfo { get; init; }

    [XmlElement("JournalLocation")]
    public JournalLocation? JournalLocation { get; init; }

    [XmlElement("OperationLocation")]
    public OperationLocation? OperationLocation { get; init; }

    [XmlElement("Certificate")]
    public CertificateInfo? Certificate { get; init; }

    [XmlElement("PcxService")]
    public PcxService? PcxService { get; init; }
}

public sealed class PartnerAuthority
{
    [XmlElement("PartnerName")]
    public required string PartnerName { get; init; }

    [XmlElement("PartnerCode")]
    public required string PartnerCode { get; init; }

    [XmlElement("POSAuthorityCode")]
    public required string PosAuthorityCode { get; init; }
}

public sealed class OrganizationBranch
{
    [XmlElement("BranchName")]
    public required string BranchName { get; init; }

    [XmlElement("BranchCode")]
    public required string BranchCode { get; init; }
}

public sealed class OrganizationChain
{
    [XmlElement("ChainName")]
    public required string ChainName { get; init; }

    [XmlElement("ChainCode")]
    public required string ChainCode { get; init; }
}

public sealed class StoreInfo
{
    [XmlElement("StoreID")]
    public required string StoreId { get; init; }

    [XmlElement("StoreName")]
    public required string StoreName { get; init; }

    [XmlElement("Address")]
    public required string Address { get; init; }

    [XmlElement("Zipcode")]
    public required string Zipcode { get; init; }

    [XmlElement("City")]
    public required string City { get; init; }
}

public sealed class CompanyInfo
{
    [XmlElement("OrganizationNumber")]
    public required string OrganizationNumber { get; init; }

    [XmlElement("Company")]
    public required string Company { get; init; }

    [XmlElement("Address")]
    public required string Address { get; init; }

    [XmlElement("Zipcode")]
    public required string Zipcode { get; init; }

    [XmlElement("City")]
    public required string City { get; init; }
}

public sealed class RegisterInfo
{
    [XmlElement("RegisterID")]
    public required string RegisterId { get; init; }

    [XmlElement("RegisterMake")]
    public string? RegisterMake { get; init; }

    [XmlElement("RegisterModel")]
    public string? RegisterModel { get; init; }

    [XmlElement("LocalAlias")]
    public string? LocalAlias { get; init; }

    [XmlElement("CounterNumber")]
    public string? CounterNumber { get; init; }

    [XmlElement("Address")]
    public string? Address { get; init; }

    [XmlElement("Zipcode")]
    public string? Zipcode { get; init; }

    [XmlElement("City")]
    public string? City { get; init; }
}

public sealed class JournalLocation
{
    [XmlElement("Company")]
    public required string Company { get; init; }

    [XmlElement("Address")]
    public required string Address { get; init; }

    [XmlElement("Zipcode")]
    public required string Zipcode { get; init; }

    [XmlElement("City")]
    public required string City { get; init; }
}

public sealed class OperationLocation
{
    [XmlElement("RegisterID")]
    public string? RegisterId { get; init; }

    [XmlElement("Company")]
    public required string Company { get; init; }

    [XmlElement("Address")]
    public required string Address { get; init; }

    [XmlElement("Zipcode")]
    public required string Zipcode { get; init; }

    [XmlElement("City")]
    public required string City { get; init; }
}

public sealed class CertificateInfo
{
    [XmlElement("Method")]
    public required string Method { get; init; }

    [XmlElement("Email")]
    public required string Email { get; init; }

    [XmlElement("Cellphone")]
    public required string Cellphone { get; init; }
}

public sealed class PcxService
{
    [XmlElement("CCU")]
    public required Ccu Ccu { get; init; }

    [XmlElement("Swish")]
    public required Swish Swish { get; init; }

    [XmlElement("Sparakvittot")]
    public required Sparakvittot Sparakvittot { get; init; }
}

public sealed class Ccu
{
    [XmlElement("Enable")]
    public required string Enable { get; init; }
}

public sealed class Swish
{
    [XmlElement("Enable")]
    public required string Enable { get; init; }

    [XmlElement("SwishNr")]
    public required string SwishNr { get; init; }

    [XmlElement("SwishType")]
    public required string SwishType { get; init; }
}

public sealed class Sparakvittot
{
    [XmlElement("Enable")]
    public required string Enable { get; init; }

    [XmlElement("SparakvittotAccount")]
    public required string SparakvittotAccount { get; init; }

    [XmlElement("SparakvittotStoreid")]
    public required string SparakvittotStoreid { get; init; }

    [XmlElement("SparakvittotUsername")]
    public required string SparakvittotUsername { get; init; }

    [XmlElement("SparakvittotPassword")]
    public required string SparakvittotPassword { get; init; }
}