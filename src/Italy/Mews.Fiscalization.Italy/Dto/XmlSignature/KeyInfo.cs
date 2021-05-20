using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.XmlSignature
{
    [Serializable, XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#"), XmlRoot("KeyInfo", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public class KeyInfo
    {
        [XmlAnyElement, XmlElement("KeyName", typeof(string)), XmlElement("KeyValue", typeof(KeyValue)), XmlElement("MgmtData", typeof(string)), XmlElement("PGPData", typeof(PGPData)), XmlElement("RetrievalMethod", typeof(RetrievalMethod)), XmlElement("SPKIData", typeof(SPKIData)), XmlElement("X509Data", typeof(X509Data)), XmlChoiceIdentifier("ItemsElementName")]
        public object[] Items { get; set; }

        [XmlElement("ItemsElementName"), XmlIgnore]
        public ItemsChoice3[] ItemsElementName { get; set; }

        [XmlText]
        public string[] Text { get; set; }

        [XmlAttribute(DataType = "ID")]
        public string Id { get; set; }
    }
}