using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.XmlSignature
{
    [Serializable, XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#"), XmlRoot("KeyValue", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public class KeyValue
    {
        [XmlAnyElement, XmlElement("DSAKeyValue", typeof(DSAKeyValue)), XmlElement("RSAKeyValue", typeof(RSAKeyValue))]
        public object Item { get; set; }

        [XmlText]
        public string[] Text { get; set; }
    }
}