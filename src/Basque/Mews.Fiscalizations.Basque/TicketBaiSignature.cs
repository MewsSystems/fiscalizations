using FuncSharp;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace Mews.Fiscalizations.Basque
{
    internal static class TicketBaiSignature
    {
        static TicketBaiSignature()
        {
            Id = Guid.NewGuid();
        }

        public static Guid Id { get; }

        public static string NamespaceUri => "http://uri.etsi.org/01903/v1.3.2#";

        public static void Sign(XmlDocument xmlDoc, X509Certificate2 certificate, Region region)
        {
            var signedXml = new XAdESSignedXml(xmlDoc);
            signedXml.Signature.Id = $"id-{Id}";
            signedXml.Signature.SignedInfo.CanonicalizationMethod = SignedXml.XmlDsigExcC14NTransformUrl;
            signedXml.SignedInfo.SignatureMethod = SignedXml.XmlDsigRSASHA256Url;

            var qualifyingPropertiesRoot = xmlDoc.CreateElement("xades", "QualifyingProperties", NamespaceUri);
            qualifyingPropertiesRoot.SetAttribute("Target", $"#id-{Id}");

            var signaturePropertiesRoot = xmlDoc.CreateElement("xades", "SignedProperties", NamespaceUri);
            signaturePropertiesRoot.SetAttribute("Id", $"xades-id-{Id}");

            var signedSignatureProperties = xmlDoc.CreateElement("xades", "SignedSignatureProperties", NamespaceUri);

            var timestamp = xmlDoc.CreateElement("xades", "SigningTime", NamespaceUri);
            timestamp.InnerText = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

            var signaturePolicyIdentifier = xmlDoc.CreateElement("xades", "SignaturePolicyIdentifier", NamespaceUri);
            var signaturePolicyId = xmlDoc.CreateElement("xades", "SignaturePolicyId", NamespaceUri);
            var sigPolicyId = xmlDoc.CreateElement("xades", "SigPolicyId", NamespaceUri);

            var sigPolicyIdentifier = xmlDoc.CreateElement("xades", "Identifier", NamespaceUri);
            sigPolicyIdentifier.InnerText = "urn:ticketbai:dss:policy:1";

            var sigPolicyDescription = xmlDoc.CreateElement("xades", "Description", NamespaceUri);
            sigPolicyDescription.InnerText = "TicketBAI sinadura-politika / Politica de firma TicketBAI";

            var sigPolicyHash = xmlDoc.CreateElement("xades", "SigPolicyHash", NamespaceUri);

            var sigPolicyHashDigestMethod = xmlDoc.CreateElement("dsig", "DigestMethod", NamespaceUri);
            sigPolicyHashDigestMethod.SetAttribute("Algorithm", SignedXml.XmlDsigSHA256Url);

            var sigPolicyHashDigestValue = xmlDoc.CreateElement("dsig", "DigestValue", NamespaceUri);
            sigPolicyHashDigestValue.InnerText = region.Match(
                Region.Gipuzkoa, _ => "vSe1CH7eAFVkGN0X2Y7Nl9XGUoBnziDA5BGUSsyt8mg=",
                Region.Araba, _ => "4Vk3uExj7tGn9DyUCPDsV9HRmK6KZfYdRiW3StOjcQA="
            );

            var sigPolicyQualifiers = xmlDoc.CreateElement("xades", "SigPolicyQualifiers", NamespaceUri);
            var sigPolicyQualifier = xmlDoc.CreateElement("xades", "SigPolicyQualifier", NamespaceUri);

            var SPURI = xmlDoc.CreateElement("xades", "SPURI", NamespaceUri);
            SPURI.InnerText = region.Match(
                Region.Gipuzkoa, _ => "https://www.gipuzkoa.eus/ticketbai/sinadura",
                Region.Araba, _ => "https://ticketbai.araba.eus/tbai/sinadura/"
            );

            var signedDataObjectProperties = xmlDoc.CreateElement("xades", "SignedDataObjectProperties", NamespaceUri);
            var dataObjectFormat = xmlDoc.CreateElement("xades", "DataObjectFormat", NamespaceUri);
            dataObjectFormat.SetAttribute("ObjectReference", $"#Id-{Id}");

            var mimeType = xmlDoc.CreateElement("xades", "MimeType", NamespaceUri);
            mimeType.InnerText = "application/octet-stream";

            signedSignatureProperties.AppendChild(timestamp);
            sigPolicyQualifiers.AppendChild(sigPolicyQualifier);
            sigPolicyQualifier.AppendChild(SPURI);
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
            dataObjectFormat.AppendChild(mimeType);
            signaturePropertiesRoot.AppendChild(signedSignatureProperties);
            signaturePropertiesRoot.AppendChild(signedDataObjectProperties);
            qualifyingPropertiesRoot.AppendChild(signaturePropertiesRoot);

            var dataObject = new DataObject
            {
                Data = qualifyingPropertiesRoot.SelectNodes("."),
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
            var certificateData = new KeyInfoX509Data(certificate);
            var issuerSerial = new X509IssuerSerial
            {
                IssuerName = certificate.IssuerName.Name,
                SerialNumber = certificate.SerialNumber
            };
            certificateData.AddIssuerSerial(issuerSerial.IssuerName, issuerSerial.SerialNumber);

            var keyInfo = new KeyInfo();
            keyInfo.AddClause(certificateData);
            signedXml.KeyInfo = keyInfo;
        }

        private static void AddReferences(XAdESSignedXml signedXml)
        {
            var reference1 = new Reference
            {
                Type = "",
                Uri = $"",
                Id = $"Id-{Id}"
            };
            reference1.AddTransform(new XmlDsigEnvelopedSignatureTransform());
            reference1.AddTransform(new XmlDsigExcC14NTransform());

            var reference2 = new Reference
            {
                Type = "http://uri.etsi.org/01903#SignedProperties",
                Uri = $"#xades-id-{Id}"
            };
            reference2.AddTransform(new XmlDsigExcC14NTransform());

            signedXml.AddReference(reference1);
            signedXml.AddReference(reference2);
        }
    }
}