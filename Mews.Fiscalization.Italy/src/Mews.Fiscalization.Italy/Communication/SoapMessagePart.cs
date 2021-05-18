using System.Xml;

namespace Mews.Fiscalization.Italy.Communication
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
