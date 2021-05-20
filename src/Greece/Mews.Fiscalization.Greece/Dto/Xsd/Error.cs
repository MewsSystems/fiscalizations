using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Greece.Dto.Xsd
{
    [Serializable]
    public class Error
    {
        [XmlElement(ElementName = "message")]
        public string Message { get; set; }

        [XmlElement(ElementName = "code")]
        public string Code { get; set; }
    }
}