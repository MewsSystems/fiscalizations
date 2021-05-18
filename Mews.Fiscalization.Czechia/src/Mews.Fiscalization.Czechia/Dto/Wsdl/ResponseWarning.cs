using System;
using System.Xml.Serialization;

namespace Mews.Eet.Dto.Wsdl
{
    [Serializable]
    [XmlType(Namespace = "http://fs.mfcr.cz/eet/schema/v3")]
    public class ResponseWarning
    {
        [XmlAttribute(AttributeName = "kod_varov")]
        public int Code { get; set; }

        [XmlText]
        public string[] Text { get; set; }
    }
}
