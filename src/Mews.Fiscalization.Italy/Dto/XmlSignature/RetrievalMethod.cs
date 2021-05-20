using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.XmlSignature
{
    [Serializable, XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#"), XmlRoot("RetrievalMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public class RetrievalMethod
    {
        [XmlArrayItem("Transform", IsNullable = false)]
        public Transform[] Transforms { get; set; }

        [XmlAttribute(DataType = "anyURI")]
        public string URI { get; set; }

        [XmlAttribute(DataType = "anyURI")]
        public string Type { get; set; }
    }
}