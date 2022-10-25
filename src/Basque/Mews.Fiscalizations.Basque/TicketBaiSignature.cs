using FuncSharp;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace Mews.Fiscalizations.Basque
{
    internal static class TicketBaiSignature
    {
        private static readonly string NamespaceUri = "http://uri.etsi.org/01903/v1.3.2#";

        private static readonly string XadesPrefix = "xades";

        static TicketBaiSignature()
        {
            Id = Guid.NewGuid();
            Reference1Id = Guid.NewGuid();
        }

        private static Guid Id { get; }

        private static Guid Reference1Id { get; }

        public static void Sign(XmlDocument xmlDoc, X509Certificate2 certificate, Region region)
        {
            var signedXml = new XAdESSignedXml(xmlDoc);
            signedXml.Signature.Id = $"Signature-{Id}-Signature";
            signedXml.Signature.SignedInfo.CanonicalizationMethod = SignedXml.XmlDsigC14NTransformUrl;
            signedXml.SignedInfo.SignatureMethod = SignedXml.XmlDsigRSASHA256Url;

            var qualifyingPropertiesRoot = xmlDoc.CreateElement(XadesPrefix, "QualifyingProperties", NamespaceUri);
            qualifyingPropertiesRoot.SetAttribute("Target", $"#Signature-{Id}-Signature");
            qualifyingPropertiesRoot.SetAttribute("Id", $"Signature-{Id}-QualifyingProperties");
            qualifyingPropertiesRoot.SetAttribute("xmlns:ds", "http://www.w3.org/2000/09/xmldsig#");

            var signaturePropertiesRoot = xmlDoc.CreateElement(XadesPrefix, "SignedProperties", NamespaceUri);
            signaturePropertiesRoot.SetAttribute("Id", $"Signature-{Id}-SignedProperties");

            var signedSignatureProperties = xmlDoc.CreateElement(XadesPrefix, "SignedSignatureProperties", NamespaceUri);

            var timestamp = xmlDoc.CreateElement(XadesPrefix, "SigningTime", NamespaceUri);
            timestamp.InnerText = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

            var signingCertificate = xmlDoc.CreateElement(XadesPrefix, "SigningCertificate", NamespaceUri);
            var cert = xmlDoc.CreateElement(XadesPrefix, "Cert", NamespaceUri);
            var certDigest = xmlDoc.CreateElement(XadesPrefix, "CertDigest", NamespaceUri);
            var certDigestMethod = xmlDoc.CreateElement(XadesPrefix, "DigestMethod", "");
            certDigestMethod.SetAttribute("Algorithm", SignedXml.XmlDsigSHA256Url);
            var certDigestValue = xmlDoc.CreateElement(XadesPrefix, "DigestValue", "");
            certDigestValue.InnerText = certificate.GetCertHashString();
            var issuerSerial = xmlDoc.CreateElement(XadesPrefix, "IssuerSerial", NamespaceUri);
            var x509IssuerName = xmlDoc.CreateElement("ds", "X509IssuerName", "");
            x509IssuerName.InnerText = certificate.IssuerName.Name;
            var x509SerialNumber = xmlDoc.CreateElement("ds", "X509SerialNumber", "");
            x509SerialNumber.InnerText = certificate.GetSerialNumberString();

            var signaturePolicyIdentifier = xmlDoc.CreateElement(XadesPrefix, "SignaturePolicyIdentifier", NamespaceUri);
            var signaturePolicyId = xmlDoc.CreateElement(XadesPrefix, "SignaturePolicyId", NamespaceUri);
            var sigPolicyId = xmlDoc.CreateElement(XadesPrefix, "SigPolicyId", NamespaceUri);

            var sigPolicyIdentifier = xmlDoc.CreateElement(XadesPrefix, "Identifier", NamespaceUri);
            sigPolicyIdentifier.InnerText = "https://www.gipuzkoa.eus/ticketbai/sinadura";

            var sigPolicyDescription = xmlDoc.CreateElement(XadesPrefix, "Description", NamespaceUri);

            var sigPolicyHash = xmlDoc.CreateElement(XadesPrefix, "SigPolicyHash", NamespaceUri);

            var sigPolicyHashDigestMethod = xmlDoc.CreateElement("ds", "DigestMethod", "");
            sigPolicyHashDigestMethod.SetAttribute("Algorithm", SignedXml.XmlDsigSHA256Url);

            var sigPolicyHashDigestValue = xmlDoc.CreateElement("ds", "DigestValue", "");
            sigPolicyHashDigestValue.InnerText = region.Match(
                Region.Gipuzkoa, _ => "vSe1CH7eAFVkGN0X2Y7Nl9XGUoBnziDA5BGUSsyt8mg=",
                Region.Araba, _ => "4Vk3uExj7tGn9DyUCPDsV9HRmK6KZfYdRiW3StOjcQA="
            );

            var sigPolicyQualifiers = xmlDoc.CreateElement(XadesPrefix, "SigPolicyQualifiers", NamespaceUri);
            var sigPolicyQualifier = xmlDoc.CreateElement(XadesPrefix, "SigPolicyQualifier", NamespaceUri);

            var Spuri = xmlDoc.CreateElement(XadesPrefix, "SPURI", NamespaceUri);
            Spuri.InnerText = region.Match(
                Region.Gipuzkoa, _ => "https://www.gipuzkoa.eus/ticketbai/sinadura",
                Region.Araba, _ => "https://ticketbai.araba.eus/tbai/sinadura/"
            );

            var signedDataObjectProperties = xmlDoc.CreateElement(XadesPrefix, "SignedDataObjectProperties", NamespaceUri);
            var dataObjectFormat = xmlDoc.CreateElement(XadesPrefix, "DataObjectFormat", NamespaceUri);
            dataObjectFormat.SetAttribute("ObjectReference", $"#Reference-{Reference1Id}");

            var description = xmlDoc.CreateElement(XadesPrefix, "Description", NamespaceUri);

            var objectIdentifier = xmlDoc.CreateElement(XadesPrefix, "ObjectIdentifier", NamespaceUri);

            var identifier = xmlDoc.CreateElement(XadesPrefix, "Identifier", NamespaceUri);
            identifier.SetAttribute("Qualifier", "OIDAsURN");
            identifier.InnerText = "urn:oid:1.2.840.10003.5.109.10";

            var mimeType = xmlDoc.CreateElement(XadesPrefix, "MimeType", NamespaceUri);
            mimeType.InnerText = "text/xml";

            var encoding = xmlDoc.CreateElement(XadesPrefix, "Encoding", NamespaceUri);

            signedSignatureProperties.AppendChild(timestamp);
            signedSignatureProperties.AppendChild(signingCertificate);
            signingCertificate.AppendChild(cert);
            cert.AppendChild(certDigest);
            certDigest.AppendChild(certDigestMethod);
            certDigest.AppendChild(certDigestValue);
            cert.AppendChild(issuerSerial);
            issuerSerial.AppendChild(x509IssuerName);
            issuerSerial.AppendChild(x509SerialNumber);
            sigPolicyQualifiers.AppendChild(sigPolicyQualifier);
            sigPolicyQualifier.AppendChild(Spuri);
            signaturePolicyIdentifier.AppendChild(signaturePolicyId);
            signaturePolicyId.AppendChild(sigPolicyId);
            sigPolicyId.AppendChild(sigPolicyIdentifier);
            sigPolicyId.AppendChild(sigPolicyDescription);
            signaturePolicyId.AppendChild(sigPolicyHash);
            signaturePolicyId.AppendChild(sigPolicyQualifiers);
            sigPolicyHash.AppendChild(sigPolicyHashDigestMethod);
            sigPolicyHash.AppendChild(sigPolicyHashDigestValue);
            signedSignatureProperties.AppendChild(signaturePolicyIdentifier);
            signedDataObjectProperties.AppendChild(dataObjectFormat);
            dataObjectFormat.AppendChild(description);
            dataObjectFormat.AppendChild(objectIdentifier);
            objectIdentifier.AppendChild(identifier);
            objectIdentifier.AppendChild(description);
            dataObjectFormat.AppendChild(mimeType);
            dataObjectFormat.AppendChild(encoding);
            signaturePropertiesRoot.AppendChild(signedSignatureProperties);
            signaturePropertiesRoot.AppendChild(signedDataObjectProperties);
            qualifyingPropertiesRoot.AppendChild(signaturePropertiesRoot);

            var dataObject = new DataObject
            {
                Data = qualifyingPropertiesRoot.SelectNodes(".")
            };
            signedXml.AddObject(dataObject);
            signedXml.SigningKey = certificate.PrivateKey;

            AddKeyInfo(signedXml, certificate);
            AddReferences(signedXml);

            signedXml.ComputeSignature();

            var xmlDigitalSignature = signedXml.GetXml();
            xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, deep: true));
        }

        private static void AddKeyInfo(XAdESSignedXml signedXml, X509Certificate2 certificate)
        {
            var certificateData = new KeyInfoX509Data(certificate, X509IncludeOption.WholeChain);
            var keyInfo = new KeyInfo();
            keyInfo.Id = $"Signature-{Id}-KeyInfo";
            keyInfo.AddClause(certificateData);
            keyInfo.AddClause(new RSAKeyValue(certificate.GetRSAPrivateKey()));
            signedXml.KeyInfo = keyInfo;
        }

        private static void AddReferences(XAdESSignedXml signedXml)
        {
            var reference1 = new Reference
            {
                Uri = "",
                Id = $"Reference-{Reference1Id}"
            };
            reference1.AddTransform(new XmlDsigC14NTransform());
            reference1.AddTransform(new XmlDsigEnvelopedSignatureTransform());
            reference1.AddTransform(CreateXPathTransform("not(ancestor-or-self::ds:Signature)"));

            var reference2 = new Reference
            {
                Type = "http://uri.etsi.org/01903#SignedProperties",
                Uri = $"#Signature-{Id}-SignedProperties"
            };
            
            var reference3 = new Reference
            {
                Uri = $"#Signature-{Id}-KeyInfo"
            };

            signedXml.AddReference(reference1);
            signedXml.AddReference(reference2);
            signedXml.AddReference(reference3);
        }

        private static XmlDsigXPathTransform CreateXPathTransform(string XPathString)
        {
            var doc = new XmlDocument();
            var xPathElem = doc.CreateElement(XadesPrefix, "XPath", NamespaceUri);
            xPathElem.SetAttribute("xmlns:ds", "http://www.w3.org/2000/09/xmldsig#");
            xPathElem.InnerText = XPathString;

            var xForm = new XmlDsigXPathTransform();
            xForm.LoadInnerXml(xPathElem.SelectNodes("."));
            return xForm;
        }
    }
}