using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.XmlSignature
{
    [Serializable, XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#"), XmlRoot("RSAKeyValue", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public class RSAKeyValue
    {
        [XmlElement(DataType = "base64Binary")]
        public byte[] Modulus { get; set; }

        [XmlElement(DataType = "base64Binary")]
        public byte[] Exponent { get; set; }
    }
}