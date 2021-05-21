using System.Xml;

namespace Mews.Eet.Communication
{
    public class SoapMessagePart
    {
        public SoapMessagePart(XmlElement xmlElement)
        {
            XmlElement = xmlElement;
        }

        public XmlElement XmlElement { get; }
    }
}
