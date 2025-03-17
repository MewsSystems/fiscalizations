using System.Xml.Serialization;

namespace Mews.Fiscalizations.Sweden.DTOs;

[XmlRoot("TcsResponse")]
public sealed class RegisterStatusResponse
{
    [XmlElement("ResponseCode")]
    public required int ResponseCode { get; init; }

    [XmlElement("ResponseMessage")]
    public required string ResponseMessage { get; init; }

    [XmlElement("ResponseReason")]
    public required string ResponseReason { get; init; }

    [XmlElement("ApplicationID")]
    public required string ApplicationId { get; init; }

    [XmlElement("RequestID")]
    public required string RequestId { get; init; }

    [XmlElement("SKVResponseCode")]
    public required string SKVResponseCode { get; init; }

    [XmlElement("SKVResponseMessage")]
    public required string SKVResponseMessage { get; init; }
}