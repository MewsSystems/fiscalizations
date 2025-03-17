using System.Xml.Serialization;

namespace Mews.Fiscalizations.Sweden.DTOs;

[XmlRoot("TcsRequest")]
public sealed class RegisterStatusRequest
{
    [XmlElement("ApplicationID")]
    public required string ApplicationId { get; init; }

    [XmlElement("RequestID")]
    public required string RequestId { get; init; }

    [XmlElement("RegisterStatus")]
    public required RegisterStatusData RegisterStatus { get; init; }
}

public sealed class RegisterStatusData
{
    [XmlElement("OrgNr")]
    public required long OrgNr { get; init; }

    [XmlElement("ManRegisterID")]
    public required string ManRegisterId { get; init; }
}