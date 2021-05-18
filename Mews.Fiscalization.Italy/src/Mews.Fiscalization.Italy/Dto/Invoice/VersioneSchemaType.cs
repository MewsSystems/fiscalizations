using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.Invoice
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