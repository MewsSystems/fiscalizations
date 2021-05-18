using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.XmlSignature
{
    [Serializable, XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#"), XmlRoot("X509Data", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public class X509Data
    {
        [XmlAnyElement, XmlElement("X509CRL", typeof(byte[]), DataType = "base64Binary"), XmlElement("X509Certificate", typeof(byte[]), DataType = "base64Binary"), XmlElement("X509IssuerSerial", typeof(X509IssuerSerial)), XmlElement("X509SKI", typeof(byte[]), DataType = "base64Binary"), XmlElement("X509SubjectName", typeof(string)), XmlChoiceIdentifier("ItemsElementName")]
        public object[] Items { get; set; }

        [XmlElement("ItemsElementName"), XmlIgnore]
        public ItemsChoice1[] ItemsElementName { get; set; }
    }
}