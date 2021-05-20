using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.XmlSignature
{
    [Serializable, XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#"), XmlRoot("SignatureProperties", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public class SignatureProperties
    {
        [XmlElement("SignatureProperty")]
        public SignatureProperty[] SignatureProperty { get; set; }

        [XmlAttribute(DataType = "ID")]
        public string Id { get; set; }
    }
}