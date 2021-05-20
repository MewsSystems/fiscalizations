using System;
using System.Xml;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.XmlSignature
{
    [Serializable, XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#"), XmlRoot("Object", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public class SignatureObject
    {
        [XmlText, XmlAnyElement]
        public XmlNode[] Any { get; set; }

        [XmlAttribute(DataType = "ID")]
        public string Id { get; set; }

        [XmlAttribute]
        public string MimeType { get; set; }

        [XmlAttribute(DataType = "anyURI")]
        public string Encoding { get; set; }
    }
}