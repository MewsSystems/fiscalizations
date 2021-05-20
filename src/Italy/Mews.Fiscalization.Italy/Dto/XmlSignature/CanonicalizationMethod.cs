using System;
using System.Xml;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.XmlSignature
{
    [Serializable, XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#"), XmlRoot("CanonicalizationMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public class CanonicalizationMethod
    {
        [XmlText, XmlAnyElement]
        public XmlNode[] Any { get; set; }

        [XmlAttribute(DataType = "anyURI")]
        public string Algorithm { get; set; }
    }
}