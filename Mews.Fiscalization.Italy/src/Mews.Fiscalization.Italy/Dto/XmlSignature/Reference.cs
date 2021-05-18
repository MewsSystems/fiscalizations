using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.XmlSignature
{
    [Serializable, XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#"), XmlRoot("Reference", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public class Reference
    {
        [XmlArrayItem("Transform", IsNullable = false)]
        public Transform[] Transforms { get; set; }

        public DigestMethod DigestMethod { get; set; }

        [XmlElement(DataType = "base64Binary")]
        public byte[] DigestValue { get; set; }

        [XmlAttribute(DataType = "ID")]
        public string Id { get; set; }

        [XmlAttribute(DataType = "anyURI")]
        public string URI { get; set; }

        [XmlAttribute(DataType = "anyURI")]
        public string Type { get; set; }
    }
}