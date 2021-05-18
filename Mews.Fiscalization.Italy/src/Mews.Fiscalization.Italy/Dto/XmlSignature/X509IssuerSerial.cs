using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.XmlSignature
{
    [Serializable, XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public class X509IssuerSerial
    {
        public string X509IssuerName { get; set; }

        [XmlElement(DataType = "integer")]
        public string X509SerialNumber { get; set; }
    }
}