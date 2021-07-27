using System;
using System.Xml.Serialization;

namespace Mews.Fiscalizations.Uniwix.Dto.Invoice
{
    [Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
    public enum ShareholderDistribution
    {
        [XmlEnum("SU")]
        SingleShareholder,
        [XmlEnum("SM")]
        MultipleShareholders
    }
}