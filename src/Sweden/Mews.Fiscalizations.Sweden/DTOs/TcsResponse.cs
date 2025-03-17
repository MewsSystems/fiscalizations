using System.Xml.Serialization;

namespace Mews.Fiscalizations.Sweden.DTOs;

[XmlRoot("TcsResponse")]
public sealed class TcsResponse
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

    [XmlElement("SequenceNumber")]
    public required string SequenceNumber { get; init; }

    [XmlElement("ControlCode")]
    public required ControlCode ControlCode { get; init; }
}

public sealed class ControlCode
{
    [XmlElement("ControlServerID")]
    public required string ControlServerId { get; init; }

    [XmlElement("Code")]
    public required string Code { get; init; }
}