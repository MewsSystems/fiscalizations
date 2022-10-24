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

        private static readonly string DsigPrefix = "dsig";

        static TicketBaiSignature()
        {
            Id = Guid.NewGuid();
        }

        private static Guid Id { get; }

        public static void Sign(XmlDocument xmlDoc, X509Certificate2 certificate, Region region)
        {
            var signedXml = new XAdESSignedXml(xmlDoc);
            signedXml.Signature.Id = $"id-{Id}";
            signedXml.Signature.SignedInfo.CanonicalizationMethod = SignedXml.XmlDsigC14NTransformUrl;
            signedXml.SignedInfo.SignatureMethod = SignedXml.XmlDsigRSASHA256Url;

            var qualifyingPropertiesRoot = xmlDoc.CreateElement(XadesPrefix, "QualifyingProperties", NamespaceUri);
            qualifyingPropertiesRoot.SetAttribute("Target", $"#id-{Id}");

            var signaturePropertiesRoot = xmlDoc.CreateElement(XadesPrefix, "SignedProperties", NamespaceUri);
            signaturePropertiesRoot.SetAttribute("Id", $"{XadesPrefix}-id-{Id}");

            var signedSignatureProperties = xmlDoc.CreateElement(XadesPrefix, "SignedSignatureProperties", NamespaceUri);

            var timestamp = xmlDoc.CreateElement(XadesPrefix, "SigningTime", NamespaceUri);
            timestamp.InnerText = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

            var signaturePolicyIdentifier = xmlDoc.CreateElement(XadesPrefix, "SignaturePolicyIdentifier", NamespaceUri);
            var signaturePolicyId = xmlDoc.CreateElement(XadesPrefix, "SignaturePolicyId", NamespaceUri);
            var sigPolicyId = xmlDoc.CreateElement(XadesPrefix, "SigPolicyId", NamespaceUri);

            var sigPolicyIdentifier = xmlDoc.CreateElement(XadesPrefix, "Identifier", NamespaceUri);
            sigPolicyIdentifier.InnerText = "urn:ticketbai:dss:policy:1";

            var sigPolicyDescription = xmlDoc.CreateElement(XadesPrefix, "Description", NamespaceUri);
            sigPolicyDescription.InnerText = "TicketBAI sinadura-politika / Politica de firma TicketBAI";

            var sigPolicyHash = xmlDoc.CreateElement(XadesPrefix, "SigPolicyHash", NamespaceUri);

            var sigPolicyHashDigestMethod = xmlDoc.CreateElement(DsigPrefix, "DigestMethod", NamespaceUri);
            sigPolicyHashDigestMethod.SetAttribute("Algorithm", SignedXml.XmlDsigSHA256Url);

            var sigPolicyHashDigestValue = xmlDoc.CreateElement(DsigPrefix, "DigestValue", NamespaceUri);
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
            dataObjectFormat.SetAttribute("ObjectReference", $"#Id-{Id}");

            var mimeType = xmlDoc.CreateElement(XadesPrefix, "MimeType", NamespaceUri);
            mimeType.InnerText = "application/octet-stream";

            signedSignatureProperties.AppendChild(timestamp);
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
            certificateData.AddSubjectName(certificate.Subject);

            var keyInfo = new KeyInfo();
            keyInfo.AddClause(certificateData);
            signedXml.KeyInfo = keyInfo;
        }

        private static void AddReferences(XAdESSignedXml signedXml)
        {
            var reference1 = new Reference
            {
                Type = "",
                Uri = "",
                Id = $"Id-{Id}"
            };
            reference1.AddTransform(new XmlDsigEnvelopedSignatureTransform());

            var reference2 = new Reference
            {
                Type = "http://uri.etsi.org/01903#SignedProperties",
                Uri = $"#xades-id-{Id}"
            };
            reference2.AddTransform(new XmlDsigC14NTransform());

            signedXml.AddReference(reference1);
            signedXml.AddReference(reference2);
        }
    }
}