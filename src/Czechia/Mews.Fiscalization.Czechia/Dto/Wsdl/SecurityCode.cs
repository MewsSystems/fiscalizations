using System;
using System.Xml.Serialization;

namespace Mews.Eet.Dto.Wsdl
{
    [Serializable]
    [XmlType(Namespace = "http://fs.mfcr.cz/eet/schema/v3")]
    public class SecurityCode
    {
        [XmlAttribute(AttributeName = "digest")]
        public SecurityCodeDigestType Digest { get; set; }

        [XmlAttribute(AttributeName = "encoding")]
        public SecurityCodeEncodingType Encoding { get; set; }

        [XmlText]
        public string[] Text { get; set; }
    }
}
