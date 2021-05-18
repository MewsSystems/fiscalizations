using System;
using System.Xml;

namespace Mews.Eet.Events
{
    public class XmlMessageSerializedEventArgs : EventArgs
    {
        public XmlMessageSerializedEventArgs(XmlElement xmlElement, string billNumber)
        {
            XmlElement = xmlElement;
            BillNumber = billNumber;
        }

        public XmlElement XmlElement { get; }

        public string BillNumber { get; }
    }
}
