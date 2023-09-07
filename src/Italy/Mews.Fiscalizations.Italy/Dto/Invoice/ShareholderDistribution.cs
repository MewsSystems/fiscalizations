using System.Xml.Serialization;

namespace Mews.Fiscalizations.Italy.Dto.Invoice;

[Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
public enum ShareholderDistribution
{
    [XmlEnum("SU")]
    SingleShareholder,
    [XmlEnum("SM")]
    MultipleShareholders
}