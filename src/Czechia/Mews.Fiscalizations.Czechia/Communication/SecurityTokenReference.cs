using System;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace Mews.Eet.Communication
{
    public class SecurityTokenReference : KeyInfoClause
    {
        public SecurityTokenReference(string binarySecurityTokenId)
        {
            BinarySecurityTokenId = binarySecurityTokenId;
        }

        private string BinarySecurityTokenId { get; }

        public override XmlElement GetXml()
        {
            var xmlDocument = new XmlDocument();
            var tokenReferenceElement = xmlDocument.CreateElement("wse", "SecurityTokenReference", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
            xmlDocument.AppendChild(tokenReferenceElement);

            var referenceElement = xmlDocument.CreateElement("wse", "Reference", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
            referenceElement.SetAttribute("URI", "#" + BinarySecurityTokenId);
            referenceElement.SetAttribute("ValueType", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509");

            tokenReferenceElement.AppendChild(referenceElement);

            return tokenReferenceElement;
        }

        public override void LoadXml(XmlElement element)
        {
            throw new NotImplementedException();
        }
    }
}
