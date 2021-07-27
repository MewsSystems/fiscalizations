using System;
using System.Xml.Serialization;

namespace Mews.Fiscalizations.Uniwix.Dto.Invoice
{
    [Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
    public enum VersioneSchemaType
    {
        [XmlEnum("FPA12")]
        FPA12,
        [XmlEnum("FPR12")]
        FPR12
    }
}