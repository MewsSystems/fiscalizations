using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.XmlSignature
{
    [Serializable, XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#"), XmlRoot("Transform", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public class Transform
    {
        [XmlAnyElement, XmlElement("XPath", typeof(string))]
        public object[] Items { get; set; }

        [XmlText]
        public string[] Text { get; set; }

        [XmlAttribute(DataType = "anyURI")]
        public string Algorithm { get; set; }
    }
}