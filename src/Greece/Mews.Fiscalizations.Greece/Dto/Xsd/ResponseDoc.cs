using System;
using System.Xml.Serialization;

namespace Mews.Fiscalizations.Greece.Dto.Xsd
{
    [Serializable]
    [XmlRoot(ElementName = "ResponseDoc")]
    public class ResponseDoc
    {
        [XmlElement(ElementName = "response")]
        public Response[] Responses { get; set; }
    }
}