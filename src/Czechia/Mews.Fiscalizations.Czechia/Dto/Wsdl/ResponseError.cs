using System;
using System.Xml.Serialization;

namespace Mews.Eet.Dto.Wsdl
{
    [Serializable]
    [XmlType(Namespace = "http://fs.mfcr.cz/eet/schema/v3")]
    public class ResponseError
    {
        [XmlAttribute(AttributeName = "kod")]
        public int Code { get; set; }

        [XmlAttribute(AttributeName = "test")]
        public bool IsPlayground { get; set; }

        [XmlIgnore]
        public bool IsPlaygroundSpecified { get; set; }

        [XmlText]
        public string[] Text { get; set; }
    }
}
