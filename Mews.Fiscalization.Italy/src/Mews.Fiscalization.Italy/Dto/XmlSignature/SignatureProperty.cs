using System;
using System.Xml;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.XmlSignature
{
    [Serializable, XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#"), XmlRoot("SignatureProperty", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public class SignatureProperty
    {
        [XmlAnyElement]
        public XmlElement[] Items { get; set; }

        [XmlText]
        public string[] Text { get; set; }

        [XmlAttribute(DataType = "anyURI")]
        public string Target { get; set; }

        [XmlAttribute(DataType = "ID")]
        public string Id { get; set; }
    }
}