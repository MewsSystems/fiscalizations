using System;
using System.Xml;

namespace Mews.Fiscalization.Italy.Communication
{
    public class SoapMessage
    {
        public SoapMessage(SoapMessagePart body)
            : this(null, body)
        {
        }

        public SoapMessage(SoapMessagePart header, SoapMessagePart body)
        {
            Header = header;
            Body = body ?? throw new ArgumentException("No body found.");
        }

        public SoapMessagePart Body { get; }

        private SoapMessagePart Header { get; }

        public static SoapMessage FromSoapXml(XmlDocument document)
        {
            var ns = new XmlNamespaceManager(document.NameTable);
            ns.AddNamespace("s", "http://schemas.xmlsoap.org/soap/envelope/");

            return new SoapMessage(
                new SoapMessagePart(document.DocumentElement.SelectSingleNode("//s:Header", ns) as XmlElement),
                new SoapMessagePart(document.DocumentElement.SelectSingleNode("//s:Body", ns) as XmlElement)
            );
        }

        public XmlDocument GetXmlDocument()
        {
            var xmlDocument = new XmlDocument
            {
                PreserveWhitespace = true
            };

            var soapEnvelopeElement = xmlDocument.CreateElement("s", "Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
            soapEnvelopeElement.SetAttribute("xmlns:u", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");

            var soapHeaderElement = xmlDocument.CreateElement("s", "Header", "http://schemas.xmlsoap.org/soap/envelope/");
            if (Header != null)
            {
                var importedHeader = xmlDocument.ImportNode(Header.XmlElement, true);
                soapHeaderElement.AppendChild(importedHeader);
            }
            soapEnvelopeElement.AppendChild(soapHeaderElement);

            var soapBodyElement = xmlDocument.CreateElement("s", "Body", "http://schemas.xmlsoap.org/soap/envelope/");
            soapBodyElement.SetAttribute("Id", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd", "_1");

            var importedBody = xmlDocument.ImportNode(Body.XmlElement, true);
            soapBodyElement.AppendChild(importedBody);

            soapEnvelopeElement.AppendChild(soapBodyElement);
            xmlDocument.AppendChild(soapEnvelopeElement);

            return xmlDocument;
        }
    }
}
