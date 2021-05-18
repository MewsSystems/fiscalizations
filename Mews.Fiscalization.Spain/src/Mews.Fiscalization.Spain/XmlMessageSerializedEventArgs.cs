using System;
using System.Xml;

namespace Mews.Fiscalization.Spain
{
    public class XmlMessageSerializedEventArgs : EventArgs
    {
        public XmlMessageSerializedEventArgs(XmlElement xmlElement)
        {
            XmlElement = xmlElement;
        }

        public XmlElement XmlElement { get; }
    }
}