using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.Invoice
{
    [Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
    public enum WithholdingType
    {
        [XmlEnum("RT01")]
        NaturalPerson,
        [XmlEnum("RT02")]
        LegalPerson,
    }
}