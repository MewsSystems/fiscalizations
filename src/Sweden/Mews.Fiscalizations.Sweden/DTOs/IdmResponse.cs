using System.Xml.Serialization;

namespace Mews.Fiscalizations.Sweden.DTOs;

[XmlRoot("IdmResponse")]
public class IdmResponse
{
    [XmlElement("ResponseCode")]
    public required int ResponseCode { get; init; }

    [XmlElement("ResponseMessage")]
    public required string ResponseMessage { get; init; }

    [XmlElement("ResponseReason")]
    public required string ResponseReason { get; init; }

    [XmlElement("ApplicationID")]
    public string? ApplicationId { get; init; }

    [XmlElement("RequestID")]
    public string? RequestId { get; init; }

    [XmlElement("Action")]
    public required string Action { get; init; }

    [XmlElement("RegisterID")]
    public string? RegisterId { get; init; }

    [XmlElement("CCUID")]
    public string? Ccuid { get; init; }

    [XmlElement("Active")]
    public int? Active { get; init; }

    [XmlElement("LoginCount")]
    public int? LoginCount { get; init; }

    [XmlElement("LastLogin")]
    public string? LastLogin { get; init; }

    [XmlElement("Info")]
    public Info? Info { get; init; }
}

public sealed class Info
{
    [XmlElement("PartnerAuthority")]
    public PartnerAuthority? PartnerAuthority { get; init; }

    [XmlElement("StoreInfo")]
    public StoreInfo? StoreInfo { get; init; }

    [XmlElement("CompanyInfo")]
    public CompanyInfo? CompanyInfo { get; init; }

    [XmlElement("RegisterInfo")]
    public RegisterInfo? RegisterInfo { get; init; }

    [XmlElement("JournalLocation")]
    public JournalLocation? JournalLocation { get; init; }

    [XmlElement("OperationLocation")]
    public OperationLocation? OperationLocation { get; init; }
}