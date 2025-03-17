using System.Xml.Serialization;

namespace Mews.Fiscalizations.Sweden.DTOs;

[XmlRoot("TcsRequest")]
public sealed class TcsRequest
{
    [XmlElement("ApplicationID")]
    public required string ApplicationID { get; init; }

    [XmlElement("RequestID")]
    public required Guid RequestID { get; init; }

    [XmlElement("ControlData")]
    public required ControlData ControlData { get; init; }
}

public sealed class ControlData
{
    [XmlElement("DateTime")]
    public required string DateTime { get; init; }

    [XmlElement("CopyDateTime")]
    public string? CopyDateTime { get; init; }

    public bool ShouldSerializeCopyDateTime()
    {
        return CopyDateTime is not null;
    }

    [XmlElement("OrgNr")]
    public required long OrgNr { get; init; }

    [XmlElement("ManRegisterID")]
    public required string ManRegisterID { get; init; }

    [XmlElement("RegisterAddress")]
    public required string RegisterAddress { get; init; }

    [XmlElement("SequenceNumber")]
    public required int SequenceNumber { get; init; }

    [XmlElement("CopySequenceNumber")]
    public int? CopySequenceNumber { get; init; }

    public bool ShouldSerializeCopySequenceNumber()
    {
        return CopySequenceNumber.HasValue;
    }

    [XmlElement("ReceiptType")]
    public required ReceiptType ReceiptType { get; init; }

    [XmlElement("SaleAmount")]
    public string? SaleAmount { get; init; }

    public bool ShouldSerializeSaleAmount()
    {
        return SaleAmount is not null;
    }

    [XmlElement("RefundAmount")]
    public string? RefundAmount { get; init; }

    public bool ShouldSerializeRefundAmount()
    {
        return RefundAmount is not null;
    }

    [XmlElement("VAT1")]
    public required VAT Vat25 { get; init; }

    [XmlElement("VAT2")]
    public required VAT Vat12 { get; init; }

    [XmlElement("VAT3")]
    public required VAT Vat6 { get; init; }

    [XmlElement("VAT4")]
    public required VAT Vat0 { get; init; }
}

public enum ReceiptOperationType
{
    [XmlEnum("normal")]
    Normal,
    [XmlEnum("kopia")]
    Copy,
    [XmlEnum("ovning")]
    Training,
    [XmlEnum("profo")]
    Proforma
}

public sealed class VAT
{
    [XmlElement("Percent")]
    public required string Percent { get; init; }

    [XmlElement("Amount")]
    public required string Amount { get; init; }

    [XmlElement("SubtotalAmount")]
    public required string SubtotalAmount { get; init; }
}

public sealed class ReceiptType
{
    [XmlAttribute("Type")]
    public ReceiptOperationType Type { get; init; }
}