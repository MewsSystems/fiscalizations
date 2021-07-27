using System;
using System.Xml.Serialization;

namespace Mews.Fiscalizations.Uniwix.Dto.Invoice
{
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
}