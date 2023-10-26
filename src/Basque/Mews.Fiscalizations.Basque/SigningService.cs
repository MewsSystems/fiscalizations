using System.Globalization;
using System.Numerics;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace Mews.Fiscalizations.Basque;

// Just a temporary code that signs a given XML using XAdES-BES until we have a better solution.
// The generated signature was validated using https://ec.europa.eu/digital-building-blocks/DSS/webapp-demo/validation tool and the result is "Total success".
// Source: https://github.com/soonthana/XAdES/blob/master/Src/XAdES/XAdESSignature.cs
internal static  class SigningService
{
    private static readonly string XmlDsigSignatureProperties = "http://uri.etsi.org/01903#SignedProperties";
    private static readonly string XadesPrefix = "xades";
    private static readonly string SignaturePrefix = "ds";
    private static readonly string SignatureNamespace = "http://www.w3.org/2000/09/xmldsig#";
    private static readonly string XadesNamespace = "http://uri.etsi.org/01903/v1.3.2#";
    private static readonly string SignatureId = $"xmldsig-{Guid.NewGuid()}";
    private static readonly string SignaturePropertiesId = $"{SignatureId}-SignedProperties";

    public static XmlElement SignXmlWithXadesBes(X509Certificate2 certificate, XmlDocument document, Region region)
    {
        var signedXml = new SignedXml(document)
        {
            SigningKey = certificate.GetRSAPrivateKey()
        };

        return ComputeSignature(signedXml, certificate, document, region);
    }

    private static XmlElement ComputeSignature(SignedXml signedXml, X509Certificate2 certificate, XmlDocument document, Region region)
    {
        signedXml.SignedInfo.SignatureMethod = SignedXml.XmlDsigRSASHA512Url;
        signedXml.Signature.Id = $"{SignatureId}";

        var reference = new Reference
        {
            Uri = "",
            Id = "SignatureId-ref0",
            DigestMethod = SignedXml.XmlDsigSHA512Url
        };
        reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());
        reference.AddTransform(new XmlDsigC14NTransform());
        reference.AddTransform(CreateXPathTransform("not(ancestor-or-self::ds:Signature)"));
        signedXml.AddReference(reference);

        // 1st ComputeSignature for first <Reference><DigestValue> element - Digest message of XML data
        signedXml.ComputeSignature();

        // Keep the Digest message of XML data for first <Reference><DigestValue> element
        var signedInfoElementFirstComputeSignature = signedXml.Signature.SignedInfo.GetXml();
        var signedInfoReference1DigestValueElement = signedInfoElementFirstComputeSignature.GetElementsByTagName("DigestValue")[0].InnerText;

        // This is workaround for overcoming a bug in the library
        signedXml.SignedInfo.References.Clear();

        var objectNode = BuildNodeObject(document, certificate, region);

        var dataObject = new DataObject();
        dataObject.LoadXml(objectNode);
        signedXml.AddObject(dataObject);

        var parametersSignature = new Reference
        {
            Uri = $"#{SignaturePropertiesId}",
            Type = "http://uri.etsi.org/01903#SignedProperties",
            DigestMethod = SignedXml.XmlDsigSHA512Url
        };
        signedXml.AddReference(parametersSignature);

        // 2nd ComputeSignature for second <Reference><DigestValue> element - Digest message of Signed Properties
        signedXml.ComputeSignature();

        // Keep the Digest message of Signed Properties for second <Reference><DigestValue> element
        var signedInfoElementSecondComputeSignature = signedXml.Signature.SignedInfo.GetXml();
        var signedInfoReference2DigestValueElement = signedInfoElementSecondComputeSignature.GetElementsByTagName("DigestValue")[0].InnerText;

        // Build up <SignedInfo> element with 2 <Reference> elements
        var signedInfoNode = BuildNodeSignedInfo(document, signedInfoReference1DigestValueElement, signedInfoReference2DigestValueElement);

        // Build up <Signature> element with all child elements
        var signatureNode = BuildNodeSignature(document);
        var signatureValueNode = BuildNodeSignatureValue(document);
        var keyInfoNode = BuildNodeKeyInfo(document, certificate);
        signatureNode.AppendChild(signedInfoNode);
        signatureNode.AppendChild(signatureValueNode);
        signatureNode.AppendChild(keyInfoNode);
        signatureNode.AppendChild(objectNode);

        // Load modified <Signature> back to SignedXml's object
        signedXml.LoadXml(signatureNode);

        // This is workaround for overcoming a bug in the library
        signedXml.SignedInfo.References.Clear();

        // 3rd ComputeSignature for <SignatureValue> element - Signature of XML data with XAdES
        signedXml.ComputeSignature();

        // Get new Signature value and Replacing <SignagureValue>
        var recomputedSignatureValue = Convert.ToBase64String(signedXml.SignatureValue);
        ReplaceSignatureValue(signatureNode, recomputedSignatureValue);

        return signatureNode;
    }

    private static XmlElement BuildNodeSignature(XmlDocument document)
    {
        // <Signature>
        var signatureNode = document.CreateElement(SignaturePrefix, "Signature", SignatureNamespace);
        var signatureIdAttribute = document.CreateAttribute("Id");
        signatureIdAttribute.InnerText = SignatureId;
        signatureNode.Attributes.Append(signatureIdAttribute);
        document.DocumentElement.AppendChild(signatureNode);

        return (XmlElement)document.SelectSingleNode("//*[local-name()='Signature']");
    }

    private static XmlElement BuildNodeObject(XmlDocument document, X509Certificate2 certificate, Region region)
    {
        // <Signature><Object>
        var objectNode = document.CreateElement(SignaturePrefix, "Object", SignatureNamespace);
        document.DocumentElement.AppendChild(objectNode);

        // <Signature><Object><QualifyingProperties>
        var qualifyingPropertiesNode = document.CreateElement(XadesPrefix, "QualifyingProperties", XadesNamespace);
        var qualifyingPropertiesAttrTarget = document.CreateAttribute("Target");
        var qualifyingPropertiesAttrXAdES141 = document.CreateAttribute("xmlns:xades141");
        qualifyingPropertiesAttrTarget.Value = $"#{SignatureId}";
        qualifyingPropertiesAttrXAdES141.Value = "http://uri.etsi.org/01903/v1.4.1#";
        qualifyingPropertiesNode.Attributes.Append(qualifyingPropertiesAttrTarget);
        qualifyingPropertiesNode.Attributes.Append(qualifyingPropertiesAttrXAdES141);
        objectNode.AppendChild(qualifyingPropertiesNode);

        // <Signature><Object><QualifyingProperties><SignedProperties>
        var signedPropertiesNode = document.CreateElement(XadesPrefix, "SignedProperties", XadesNamespace);
        var signedPropertiesAttrId = document.CreateAttribute("Id");
        signedPropertiesAttrId.Value = $"{SignaturePropertiesId}";
        signedPropertiesNode.Attributes.Append(signedPropertiesAttrId);
        qualifyingPropertiesNode.AppendChild(signedPropertiesNode);

        // <Signature><Object><QualifyingProperties><SignedProperties><SignedSignatureProperties>
        var signedSignaturePropertiesNode = document.CreateElement(XadesPrefix, "SignedSignatureProperties", XadesNamespace);
        signedPropertiesNode.AppendChild(signedSignaturePropertiesNode);

        // <Signature><Object><QualifyingProperties><SignedProperties><SignedSignatureProperties><SigningTime>
        var signingTimeNode = document.CreateElement(XadesPrefix, "SigningTime", XadesNamespace);
        //signingTime.InnerText = $"{DateTime.UtcNow.ToString("s")}Z";
        signingTimeNode.InnerText = $"{DateTime.UtcNow:s}Z";
        signedSignaturePropertiesNode.AppendChild(signingTimeNode);

        // <Signature><Object><QualifyingProperties><SignedProperties><SignedSignatureProperties><SigningCertificate>
        var signingCertificateNode = document.CreateElement(XadesPrefix, "SigningCertificate", XadesNamespace);
        signedSignaturePropertiesNode.AppendChild(signingCertificateNode);

        // <Signature><Object><QualifyingProperties><SignedProperties><SignedSignatureProperties><SigningCertificate><Cert>
        var certNode = document.CreateElement(XadesPrefix, "Cert", XadesNamespace);
        signingCertificateNode.AppendChild(certNode);

        // <Signature><Object><QualifyingProperties><SignedProperties><SignedSignatureProperties><SigningCertificate><Cert><CertDigest>
        var certDigestNode = document.CreateElement(XadesPrefix, "CertDigest", XadesNamespace);
        certNode.AppendChild(certDigestNode);

        // <Signature><Object><QualifyingProperties><SignedProperties><SignedSignatureProperties><SigningCertificate><Cert><CertDigest><DigestMethod>
        var digestMethodNode = document.CreateElement(SignaturePrefix, "DigestMethod", SignatureNamespace);
        var digestMethodAttrAlgorithm = document.CreateAttribute("Algorithm");
        digestMethodAttrAlgorithm.Value = SignedXml.XmlDsigSHA512Url;
        digestMethodNode.Attributes.Append(digestMethodAttrAlgorithm);
        digestMethodNode.InnerText = "";
        certDigestNode.AppendChild(digestMethodNode);

        // <Signature><Object><QualifyingProperties><SignedProperties><SignedSignatureProperties><SigningCertificate><Cert><CertDigest><DigestValue>
        var digestValueNode = document.CreateElement(SignaturePrefix, "DigestValue", SignatureNamespace);
        digestValueNode.InnerText = Convert.ToBase64String(SHA512.Create().ComputeHash(certificate.GetRawCertData()));
        certDigestNode.AppendChild(digestValueNode);

        // <Signature><Object><QualifyingProperties><SignedProperties><SignedSignatureProperties><SigningCertificate><Cert><IssuerSerial>
        var issuerSerialNode = document.CreateElement(XadesPrefix, "IssuerSerial", XadesNamespace);
        certNode.AppendChild(issuerSerialNode);

        // <Signature><Object><QualifyingProperties><SignedProperties><SignedSignatureProperties><SigningCertificate><Cert><IssuerSerial><X509IssuerName>
        var x509IssuerNameNode = document.CreateElement(SignaturePrefix, "X509IssuerName", SignatureNamespace);
        x509IssuerNameNode.InnerText = certificate.Issuer;
        issuerSerialNode.AppendChild(x509IssuerNameNode);

        // <Signature><Object><QualifyingProperties><SignedProperties><SignedSignatureProperties><SigningCertificate><Cert><IssuerSerial><X509SerialNumber>
        var x509SerialNumberNode = document.CreateElement(SignaturePrefix, "X509SerialNumber", SignatureNamespace);
        x509SerialNumberNode.InnerText = ToDecimalString(certificate.SerialNumber);
        issuerSerialNode.AppendChild(x509SerialNumberNode);

        var signaturePolicyIdentifierNode = document.CreateElement(SignaturePrefix, "SignaturePolicyIdentifier", SignatureNamespace);
        var signaturePolicyIdNode = document.CreateElement(SignaturePrefix, "SignaturePolicyId", SignatureNamespace);
        var sigPolicyIdNode = document.CreateElement(SignaturePrefix, "SigPolicyId", SignatureNamespace);

        var sigPolicyIdentifierNode = document.CreateElement(SignaturePrefix, "Identifier", SignatureNamespace);
        sigPolicyIdentifierNode.InnerText = region.Match(
            Region.Gipuzkoa, _ => "https://www.gipuzkoa.eus/ticketbai/sinadura",
            Region.Araba, _ => "https://ticketbai.araba.eus/tbai/sinadura/",
            Region.Bizkaia, _ => "https://www.batuz.eus/fitxategiak/batuz/ticketbai/sinadura_elektronikoaren_zehaztapenak_especificaciones_de_la_firma_electronica_v1_0.pdf"
        );

        var sigPolicyDescriptionNode = document.CreateElement(SignaturePrefix, "Description", SignatureNamespace);
        sigPolicyDescriptionNode.InnerText = "TicketBAI sinadura-politika / Politica de firma TicketBAI";

        var sigPolicyHashNode = document.CreateElement(SignaturePrefix, "SigPolicyHash", SignatureNamespace);

        var sigPolicyHashDigestMethodNode = document.CreateElement(SignaturePrefix, "DigestMethod", SignatureNamespace);
        sigPolicyHashDigestMethodNode.SetAttribute("Algorithm", SignedXml.XmlDsigSHA256Url);

        var sigPolicyHashDigestValueNode = document.CreateElement(SignaturePrefix, "DigestValue", SignatureNamespace);
        sigPolicyHashDigestValueNode.InnerText = region.Match(
            Region.Gipuzkoa, _ => "vSe1CH7eAFVkGN0X2Y7Nl9XGUoBnziDA5BGUSsyt8mg=",
            Region.Araba, _ => "4Vk3uExj7tGn9DyUCPDsV9HRmK6KZfYdRiW3StOjcQA=",
            Region.Bizkaia, _=> "Quzn98x3PMbSHwbUzaj5f5KOpiH0u8bvmwbbbNkO9Es="
        );

        var sigPolicyQualifiersNode = document.CreateElement(SignaturePrefix, "SigPolicyQualifiers", SignatureNamespace);
        var sigPolicyQualifierNode = document.CreateElement(SignaturePrefix, "SigPolicyQualifier", SignatureNamespace);

        var spuriNode = document.CreateElement(SignaturePrefix, "SPURI", SignatureNamespace);
        spuriNode.InnerText = region.Match(
            Region.Gipuzkoa, _ => "https://www.gipuzkoa.eus/ticketbai/sinadura",
            Region.Araba, _ => "https://ticketbai.araba.eus/tbai/sinadura/",
            Region.Bizkaia, _ => "https://www.batuz.eus/fitxategiak/batuz/ticketbai/sinadura_elektronikoaren_zehaztapenak_especificaciones_de_la_firma_electronica_v1_0.pdf"
        );

        sigPolicyQualifiersNode.AppendChild(sigPolicyQualifierNode);
        sigPolicyQualifierNode.AppendChild(spuriNode);
        signaturePolicyIdentifierNode.AppendChild(signaturePolicyIdNode);
        signaturePolicyIdNode.AppendChild(sigPolicyIdNode);
        sigPolicyIdNode.AppendChild(sigPolicyIdentifierNode);
        sigPolicyIdNode.AppendChild(sigPolicyDescriptionNode);
        signaturePolicyIdNode.AppendChild(sigPolicyHashNode);
        signaturePolicyIdNode.AppendChild(sigPolicyQualifiersNode);
        sigPolicyHashNode.AppendChild(sigPolicyHashDigestMethodNode);
        sigPolicyHashNode.AppendChild(sigPolicyHashDigestValueNode);
        signedSignaturePropertiesNode.AppendChild(signaturePolicyIdentifierNode);

        return (XmlElement)document.SelectSingleNode("//*[local-name()='Object']");
    }

    private static XmlElement BuildNodeSignedInfo(XmlDocument document, string reference1DigestValue, string reference2DigestValue)
    {
        // <Signature><SignedInfo>
        var signedInfoNode = document.CreateElement(SignaturePrefix, "SignedInfo", SignatureNamespace);
        document.DocumentElement.AppendChild(signedInfoNode);

        // <Signature><SignedInfo><CanonicalizationMethod>
        var canonicalizationMethodNode = document.CreateElement(SignaturePrefix, "CanonicalizationMethod", SignatureNamespace);
        var canonicalizationMethodAttr = document.CreateAttribute("Algorithm");
        canonicalizationMethodAttr.Value = SignedXml.XmlDsigC14NTransformUrl;
        canonicalizationMethodNode.Attributes.Append(canonicalizationMethodAttr);
        signedInfoNode.AppendChild(canonicalizationMethodNode);

        // <Signature><SignedInfo><SignatureMethod>
        var signatureMethodNode = document.CreateElement(SignaturePrefix, "SignatureMethod", SignatureNamespace);
        var signatureMethodAttr = document.CreateAttribute("Algorithm");
        signatureMethodAttr.Value = SignedXml.XmlDsigRSASHA512Url;
        signatureMethodNode.Attributes.Append(signatureMethodAttr);
        signedInfoNode.AppendChild(signatureMethodNode);

        // <Signature><SignedInfo><Reference>
        var reference1Node = document.CreateElement(SignaturePrefix, "Reference", SignatureNamespace);
        var reference1AttrId = document.CreateAttribute("Id");
        var reference1AttrURI = document.CreateAttribute("URI");
        reference1AttrId.Value = $"{SignatureId}-ref0";
        reference1AttrURI.Value = "";
        reference1Node.Attributes.Append(reference1AttrId);
        reference1Node.Attributes.Append(reference1AttrURI);
        signedInfoNode.AppendChild(reference1Node);

        // <Signature><SignedInfo><Reference><Transforms>
        var transforms1Node = document.CreateElement(SignaturePrefix, "Transforms", SignatureNamespace);
        reference1Node.AppendChild(transforms1Node);

        // <Signature><SignedInfo><Reference><Transforms><Tranform>
        var transform1Node = document.CreateElement(SignaturePrefix, "Transform", SignatureNamespace);
        var transform1Attr = document.CreateAttribute("Algorithm");
        transform1Attr.Value = SignedXml.XmlDsigEnvelopedSignatureTransformUrl;
        transform1Node.Attributes.Append(transform1Attr);
        transforms1Node.AppendChild(transform1Node);

        var transform2Node = document.CreateElement(SignaturePrefix, "Transform", SignatureNamespace);
        var transform2Attr = document.CreateAttribute("Algorithm");
        transform2Attr.Value = SignedXml.XmlDsigC14NTransformUrl;
        transform2Node.Attributes.Append(transform2Attr);
        transforms1Node.AppendChild(transform2Node);

        var transform3Node = document.CreateElement(SignaturePrefix, "Transform", SignatureNamespace);
        var transform3Attr = document.CreateAttribute("Algorithm");
        transform3Attr.Value = SignedXml.XmlDsigXPathTransformUrl;
        transform3Node.Attributes.Append(transform3Attr);

        var xPathElem = document.CreateElement(XadesPrefix, "XPath", SignatureNamespace);
        xPathElem.SetAttribute("xmlns:ds", "http://www.w3.org/2000/09/xmldsig#");
        xPathElem.InnerText = "not(ancestor-or-self::ds:Signature)";
        transform3Node.AppendChild(xPathElem);
        transforms1Node.AppendChild(transform3Node);

        // <Signature><SignedInfo><Reference><DigestMethod>
        var digestMethod1Node = document.CreateElement(SignaturePrefix, "DigestMethod", SignatureNamespace);
        var digestMethod1Attr = document.CreateAttribute("Algorithm");
        digestMethod1Attr.Value = SignedXml.XmlDsigSHA512Url;
        digestMethod1Node.Attributes.Append(digestMethod1Attr);
        reference1Node.AppendChild(digestMethod1Node);

        // <Signature><SignedInfo><Reference><DigestValue>
        var digestValue1Node = document.CreateElement(SignaturePrefix, "DigestValue", SignatureNamespace);
        digestValue1Node.InnerText = reference1DigestValue;
        reference1Node.AppendChild(digestValue1Node);

        // <Signature><SignedInfo><Reference>
        var reference2Node = document.CreateElement(SignaturePrefix, "Reference", SignatureNamespace);
        var reference2AttrType = document.CreateAttribute("Type");
        var reference2AttrURI = document.CreateAttribute("URI");
        reference2AttrType.Value = XmlDsigSignatureProperties;
        reference2AttrURI.Value = $"#{SignaturePropertiesId}";
        reference2Node.Attributes.Append(reference2AttrType);
        reference2Node.Attributes.Append(reference2AttrURI);

        // <Signature><SignedInfo><Reference><DigestMethod>
        var digestMethod2Node = document.CreateElement(SignaturePrefix, "DigestMethod", SignatureNamespace);
        var digestMethod2Attr = document.CreateAttribute("Algorithm");
        digestMethod2Attr.Value = SignedXml.XmlDsigSHA512Url;
        digestMethod2Node.Attributes.Append(digestMethod2Attr);
        digestMethod2Node.InnerText = "";
        reference2Node.AppendChild(digestMethod2Node);

        // <Signature><SignedInfo><Reference><DigestValue>
        var digestValue2Node = document.CreateElement(SignaturePrefix, "DigestValue", SignatureNamespace);
        digestValue2Node.InnerText = reference2DigestValue;
        reference2Node.AppendChild(digestValue2Node);

        signedInfoNode.AppendChild(reference2Node);

        return (XmlElement)document.SelectSingleNode("//*[local-name()='SignedInfo']");
    }

    private static XmlElement BuildNodeSignatureValue(XmlDocument document)
    {
        // <Signature><SignatureValue>
        var signatureValueNode = document.CreateElement(SignaturePrefix, "SignatureValue", SignatureNamespace);
        var signatureValueAttrId = document.CreateAttribute("Id");
        signatureValueAttrId.Value = $"{SignatureId}-sigvalue";
        signatureValueNode.InnerText = "";
        signatureValueNode.Attributes.Append(signatureValueAttrId);
        document.DocumentElement.AppendChild(signatureValueNode);

        return (XmlElement)document.SelectSingleNode("//*[local-name()='SignatureValue']");
    }

    private static XmlElement BuildNodeKeyInfo(XmlDocument document, X509Certificate2 certificate)
    {
        // <Signature><KeyInfo>
        var keyInfoNode = document.CreateElement(SignaturePrefix, "KeyInfo", SignatureNamespace);
        document.DocumentElement.AppendChild(keyInfoNode);

        // <Signature><KeyInfo><X509Data>
        var x509Node = document.CreateElement(SignaturePrefix, "X509Data", SignatureNamespace);
        keyInfoNode.AppendChild(x509Node);

        // <Signature><KeyInfo><X509Data><X509Certificate>
        var chain = new X509Chain();
        chain.Build(certificate);

        foreach (var element in chain.ChainElements)
        {
            var x509CertificateNode = document.CreateElement(SignaturePrefix, "X509Certificate", SignatureNamespace);
            x509CertificateNode.InnerText = Convert.ToBase64String(element.Certificate.GetRawCertData());
            x509Node.AppendChild(x509CertificateNode);
        }

        return (XmlElement)document.SelectSingleNode("//*[local-name()='KeyInfo']");
    }

    private static string ToDecimalString(string serialNumber)
    {
        if (BigInteger.TryParse(serialNumber, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var bi))
        {
            return bi.ToString(CultureInfo.InvariantCulture);
        }

        return serialNumber;
    }

    private static void ReplaceSignatureValue(XmlElement signature, string newValue)
    {
        ArgumentNullException.ThrowIfNull(signature, nameof(signature));
        ArgumentNullException.ThrowIfNull(signature.OwnerDocument, "OwnerDocument");

        if (string.IsNullOrEmpty(SignaturePrefix))
        {
            var signatureValue = signature.SelectSingleNode("//*[local-name()='SignatureValue']");
            ArgumentNullException.ThrowIfNull(signatureValue, "Signature does not contain 'SignatureValue'");
            signatureValue.InnerXml = newValue;
        }
        else
        {
            var namespaceManager = new XmlNamespaceManager(signature.OwnerDocument.NameTable);
            namespaceManager.AddNamespace(SignaturePrefix, SignedXml.XmlDsigNamespaceUrl);

            var signatureValue = signature.SelectSingleNode($"{SignaturePrefix}:SignatureValue", namespaceManager);
            if (signatureValue is null)
            {
                throw new Exception($"Signature does not contain '{SignaturePrefix}:SignatureValue'");
            }

            signatureValue.InnerXml = newValue;
        }
    }

    private static XmlDsigXPathTransform CreateXPathTransform(string xPathString)
    {
        var doc = new XmlDocument();
        var xPathElem = doc.CreateElement(XadesPrefix, "XPath", SignatureNamespace);
        xPathElem.SetAttribute("xmlns:ds", "http://www.w3.org/2000/09/xmldsig#");
        xPathElem.InnerText = xPathString;

        var xForm = new XmlDsigXPathTransform();
        xForm.LoadInnerXml(xPathElem.SelectNodes("."));
        return xForm;
    }
}