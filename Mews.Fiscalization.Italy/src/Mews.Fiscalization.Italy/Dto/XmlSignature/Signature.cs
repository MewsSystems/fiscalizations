using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.XmlSignature
{
    [Serializable, XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#"), XmlRoot("Signature", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public class Signature
    {
        public SignedInfo SignedInfo { get; set; }

        public SignatureValue SignatureValue { get; set; }

        public KeyInfo KeyInfo { get; set; }

        [XmlElement("Object")]
        public SignatureObject[] Object { get; set; }

        [XmlAttribute(DataType = "ID")]
        public string Id { get; set; }
    }
}