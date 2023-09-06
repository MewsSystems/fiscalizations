using System.Xml.Serialization;

namespace Mews.Fiscalizations.Italy.Dto.Invoice;

[Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
public enum PaymentTerms
{
    [XmlEnum("TP01")]
    Installments,
    [XmlEnum("TP02")]
    LumpSum,
    [XmlEnum("TP03")]
    InAdvance,
}