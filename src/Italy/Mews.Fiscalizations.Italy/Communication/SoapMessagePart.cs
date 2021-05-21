using System.Xml;

namespace Mews.Fiscalizations.Italy.Communication
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
