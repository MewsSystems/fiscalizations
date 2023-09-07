using System.Xml;

namespace Mews.Fiscalizations.Spain;

public class XmlMessageSerializedEventArgs : EventArgs
{
    public XmlMessageSerializedEventArgs(XmlElement xmlElement)
    {
        XmlElement = xmlElement;
    }

    public XmlElement XmlElement { get; }
}