using System;
using System.Xml;

namespace Mews.Fiscalizations.Spain.Communication;

internal class SoapMessage
{
    internal SoapMessage(XmlElement body)
        : this(null, body)
    {
    }

    internal SoapMessage(XmlElement header, XmlElement body)
    {
        Header = header;
        Body = body ?? throw new ArgumentException("No body found.");
    }

    internal XmlElement Body { get; }

    private XmlElement Header { get; }

    internal static SoapMessage FromSoapXml(XmlDocument document)
    {
        var ns = new XmlNamespaceManager(document.NameTable);
        ns.AddNamespace("s", "http://schemas.xmlsoap.org/soap/envelope/");

        return new SoapMessage(
            document.DocumentElement.SelectSingleNode("//s:Header", ns) as XmlElement,
            document.DocumentElement.SelectSingleNode("//s:Body", ns) as XmlElement
        );
    }

    internal XmlDocument GetXmlDocument()
    {
        var xmlDocument = new XmlDocument();
        xmlDocument.PreserveWhitespace = true;

        var soapEnvelopeElement = xmlDocument.CreateElement("s", "Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
        var soapHeaderElement = xmlDocument.CreateElement("s", "Header", "http://schemas.xmlsoap.org/soap/envelope/");
        if (Header is not null)
        {
            var importedHeader = xmlDocument.ImportNode(Header, true);
            soapHeaderElement.AppendChild(importedHeader);
        }
        soapEnvelopeElement.AppendChild(soapHeaderElement);

        var soapBodyElement = xmlDocument.CreateElement("s", "Body", "http://schemas.xmlsoap.org/soap/envelope/");
        var importedBody = xmlDocument.ImportNode(Body, true);
        soapBodyElement.AppendChild(importedBody);

        soapEnvelopeElement.AppendChild(soapBodyElement);
        xmlDocument.AppendChild(soapEnvelopeElement);

        return xmlDocument;
    }
}