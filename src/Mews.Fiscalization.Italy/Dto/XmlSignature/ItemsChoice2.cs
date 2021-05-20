using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.XmlSignature
{
    [Serializable, XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#", IncludeInSchema = false)]
    public enum ItemsChoice2
    {
        [XmlEnum("##any:")]
        Item,
        PGPKeyID,
        PGPKeyPacket,
    }
}