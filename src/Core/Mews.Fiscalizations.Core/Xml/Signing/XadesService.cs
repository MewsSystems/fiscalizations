using Mews.Fiscalizations.Core.Xml.Signing.Signature;
using Mews.Fiscalizations.Core.Xml.Signing.Signature.Parameters;
using Mews.Fiscalizations.Core.Xml.Signing.Utils;
using Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace Mews.Fiscalizations.Core.Xml.Signing;

public static class XadesService
{
    public static SignatureDocument SignEnveloped(SignatureParameters parameters)
    {
        var xmlDocumentToSign = parameters.XmlDocumentToSign;
        xmlDocumentToSign.PreserveWhitespace = true;

        var signatureDocument = new SignatureDocument();

        var dataFormat = new DataObjectFormat
        {
            MimeType = "text/xml",
            Encoding = "UTF-8"
        };
        var reference = SetContentEnveloped(signatureDocument, xmlDocumentToSign, dataFormat, parameters.ElementIdToSign);

        if (parameters.DataFormat is not null)
        {
            if (!string.IsNullOrEmpty(parameters.DataFormat.TypeIdentifier))
            {
                dataFormat.ObjectIdentifier = new ObjectIdentifier();
                dataFormat.ObjectIdentifier.Identifier.IdentifierUri = parameters.DataFormat.TypeIdentifier;
            }
            dataFormat.Description = parameters.DataFormat.Description;
        }

        SetSignatureId(signatureDocument.XadesSignature, parameters.ElementIdToSign);
        PrepareSignature(signatureDocument, parameters, reference, dataFormat);

        signatureDocument.XadesSignature.ComputeSignature();

        UpdateXadesSignature(signatureDocument);
        return signatureDocument;
    }

    private static void SetSignatureId(XadesSignedXml xadesSignedXml,string id)
    {
       // var id = Guid.NewGuid().ToString();
        xadesSignedXml.Signature.Id = $"Signature-{id}";
        xadesSignedXml.SignatureValueId = $"SignatureValue-{id}";
    }

    private static void SetSignatureDestination(SignatureDocument sigDocument, SignatureXPathExpression destination)
    {
        XmlNode node;
        if (destination.Namespaces.Count > 0)
        {
            var xmlnsMgr = new XmlNamespaceManager(sigDocument.Document.NameTable);
            foreach (var item in destination.Namespaces)
            {
                xmlnsMgr.AddNamespace(item.Key, item.Value);
            }

            node = sigDocument.Document.SelectSingleNode(destination.XPathExpression, xmlnsMgr);
        }
        else
        {
            node = sigDocument.Document.SelectSingleNode(destination.XPathExpression);
        }

        if (node == null)
        {
            throw new Exception("Item not found.");
        }

        sigDocument.XadesSignature.SignatureNodeDestination = (XmlElement)node;
    }

    private static void AddXPathTransform(SignatureDocument sigDocument, Dictionary<string, string> namespaces, string XPathString)
    {
        var document = sigDocument.Document ?? new XmlDocument();
        var xPathElem = document.CreateElement("XPath");

        foreach (var ns in namespaces)
        {
            var attr = document.CreateAttribute($"xmlns:{ns.Key}");
            attr.Value = ns.Value;
            xPathElem.Attributes.Append(attr);
        }

        xPathElem.InnerText = XPathString;

        var transform = new XmlDsigXPathTransform();
        transform.LoadInnerXml(xPathElem.SelectNodes("."));

        var reference = sigDocument.XadesSignature.SignedInfo.References[0] as Reference;
        reference.AddTransform(transform);
    }

    private static Reference SetContentEnveloped(SignatureDocument sigDocument, XmlDocument xmlDocument, DataObjectFormat dataFormat,string id)
    {
        sigDocument.Document = xmlDocument;
        var reference = new Reference();

        sigDocument.XadesSignature = new XadesSignedXml(sigDocument.Document);

        //reference.Id = $"Reference-{Guid.NewGuid()}";
        reference.Id = $"Reference-{id}";
        reference.Uri = "";

        dataFormat = new DataObjectFormat
        {
            MimeType = "text/xml",
            Encoding = "UTF-8"
        };

        for (var i = 0; i < sigDocument.Document.DocumentElement.Attributes.Count; i++)
        {
            if (sigDocument.Document.DocumentElement.Attributes[i].Name.Equals("id", StringComparison.InvariantCultureIgnoreCase))
            {
                reference.Uri = $"#{sigDocument.Document.DocumentElement.Attributes[i].Value}";
                break;
            }
        }

        var xmlDsigEnvelopedSignatureTransform = new XmlDsigEnvelopedSignatureTransform();
        reference.AddTransform(xmlDsigEnvelopedSignatureTransform);
        sigDocument.XadesSignature.AddReference(reference);
        return reference;
    }

    private static void PrepareSignature(SignatureDocument sigDocument, SignatureParameters parameters, Reference reference, DataObjectFormat dataFormat)
    {
        sigDocument.XadesSignature.SignedInfo.SignatureMethod = parameters.SignatureMethod.URI;

        AddCertificateInfo(sigDocument, parameters);
        AddXadesInfo(sigDocument, parameters, reference, dataFormat);

        foreach (Reference r in sigDocument.XadesSignature.SignedInfo.References)
        {
            r.DigestMethod = parameters.DigestMethod.URI;
        }

        if (parameters.SignatureDestination is not null)
        {
            SetSignatureDestination(sigDocument, parameters.SignatureDestination);
        }

        if (parameters.XPathTransformations.Count > 0)
        {
            foreach (var xPathTrans in parameters.XPathTransformations)
            {
                AddXPathTransform(sigDocument, xPathTrans.Namespaces, xPathTrans.XPathExpression);
            }
        }
    }

    private static void UpdateXadesSignature(SignatureDocument sigDocument)
    {
        sigDocument.UpdateDocument();
        var signatureElement = (XmlElement)sigDocument.Document.SelectSingleNode($"//*[@Id='{sigDocument.XadesSignature.Signature.Id}']");

        sigDocument.XadesSignature = new XadesSignedXml(sigDocument.Document);
        sigDocument.XadesSignature.LoadXml(signatureElement);
    }

    private static void AddXadesInfo(SignatureDocument sigDocument, SignatureParameters parameters, Reference reference, DataObjectFormat dataFormat)
    {
        var xadesObject = new XadesObject
        {
            Id = $"XadesObjectId-{Guid.NewGuid()}",
            QualifyingProperties =
            {
                Id = $"QualifyingProperties-{Guid.NewGuid()}",
                Target = $"#{sigDocument.XadesSignature.Signature.Id}",
                SignedProperties =
                {
                    Id = $"SignedProperties-{sigDocument.XadesSignature.Signature.Id}"
                }
            }
        };

        AddSignatureProperties(
            sigDocument: sigDocument,
            reference: reference,
            dataFormat: dataFormat,
            signedSignatureProperties: xadesObject.QualifyingProperties.SignedProperties.SignedSignatureProperties,
            signedDataObjectProperties: xadesObject.QualifyingProperties.SignedProperties.SignedDataObjectProperties,
            unsignedSignatureProperties: xadesObject.QualifyingProperties.UnsignedProperties.UnsignedSignatureProperties,
            parameters: parameters
        );
        sigDocument.XadesSignature.AddXadesObject(xadesObject);
    }


    private static void AddCertificateInfo(SignatureDocument sigDocument, SignatureParameters parameters)
    {
        sigDocument.XadesSignature.SigningKey = parameters.Signer.SigningKey;

        var keyInfo = new KeyInfo
        {
            Id = $"KeyInfoId-{sigDocument.XadesSignature.Signature.Id}"
        };
        keyInfo.AddClause(new KeyInfoX509Data(parameters.Signer.Certificate));
        keyInfo.AddClause(new RSAKeyValue((RSA)parameters.Signer.SigningKey));

        sigDocument.XadesSignature.KeyInfo = keyInfo;

        var reference = new Reference
        {
            Id = "ReferenceKeyInfo",
            Uri = $"#KeyInfoId-{sigDocument.XadesSignature.Signature.Id}"
        };

        sigDocument.XadesSignature.AddReference(reference);
    }


    private static void AddSignatureProperties(
        SignatureDocument sigDocument,
        Reference reference,
        DataObjectFormat dataFormat,
        SignedSignatureProperties signedSignatureProperties,
        SignedDataObjectProperties signedDataObjectProperties,
        UnsignedSignatureProperties unsignedSignatureProperties,
        SignatureParameters parameters)
    {
        var cert = new Cert
        {
            IssuerSerial =
            {
                X509IssuerName = parameters.Signer.Certificate.IssuerName.Name,
                X509SerialNumber = parameters.Signer.Certificate.GetSerialNumberAsDecimalString()
            }
        };
        DigestUtil.SetCertDigest(parameters.Signer.Certificate.GetRawCertData(), parameters.DigestMethod, cert.CertDigest);
        signedSignatureProperties.SigningCertificate.CertCollection.Add(cert);

        if (parameters.SignaturePolicyInfo is not null)
        {
            if (!string.IsNullOrEmpty(parameters.SignaturePolicyInfo.PolicyIdentifier))
            {
                signedSignatureProperties.SignaturePolicyIdentifier.SignaturePolicyImplied = false;
                signedSignatureProperties.SignaturePolicyIdentifier.SignaturePolicyId.SigPolicyId.Identifier.IdentifierUri = parameters.SignaturePolicyInfo.PolicyIdentifier;
            }

            if (!string.IsNullOrEmpty(parameters.SignaturePolicyInfo.PolicyUri))
            {
                var spq = new SigPolicyQualifier();
                spq.AnyXmlElement = sigDocument.Document.CreateElement(XadesSignedXml.XmlXadesPrefix, "SPURI", XadesSignedXml.XadesNamespaceUri);
                spq.AnyXmlElement.InnerText = parameters.SignaturePolicyInfo.PolicyUri;

                signedSignatureProperties.SignaturePolicyIdentifier.SignaturePolicyId.SigPolicyQualifiers.SigPolicyQualifierCollection.Add(spq);
            }

            if (!string.IsNullOrEmpty(parameters.SignaturePolicyInfo.PolicyHash))
            {
                signedSignatureProperties.SignaturePolicyIdentifier.SignaturePolicyId.SigPolicyHash.DigestMethod.Algorithm = parameters.SignaturePolicyInfo.PolicyDigestAlgorithm.URI;
                signedSignatureProperties.SignaturePolicyIdentifier.SignaturePolicyId.SigPolicyHash.DigestValue = Convert.FromBase64String(parameters.SignaturePolicyInfo.PolicyHash);
            }
        }

        signedSignatureProperties.SigningTime = parameters.SigningDate;

        if (dataFormat is not null)
        {
            var newDataObjectFormat = new DataObjectFormat
            {
                MimeType = dataFormat.MimeType,
                Encoding = dataFormat.Encoding,
                Description = dataFormat.Description,
                ObjectReferenceAttribute = $"#{reference.Id}"
            };

            if (dataFormat.ObjectIdentifier is not null)
            {
                newDataObjectFormat.ObjectIdentifier.Identifier.IdentifierUri = dataFormat.ObjectIdentifier.Identifier.IdentifierUri;
            }

            signedDataObjectProperties.DataObjectFormatCollection.Add(newDataObjectFormat);
        }

        if (parameters.SignerRole != null && (parameters.SignerRole.CertifiedRoles.Count > 0 || parameters.SignerRole.ClaimedRoles.Count > 0))
        {
            signedSignatureProperties.SignerRole = new Microsoft.Xades.SignerRole();
            foreach (var certifiedRole in parameters.SignerRole.CertifiedRoles)
            {
                signedSignatureProperties.SignerRole.CertifiedRoles.CertifiedRoleCollection.Add(new CertifiedRole { PkiData = certifiedRole.GetRawCertData() });
            }

            foreach (var claimedRole in parameters.SignerRole.ClaimedRoles)
            {
                signedSignatureProperties.SignerRole.ClaimedRoles.ClaimedRoleCollection.Add(new ClaimedRole { InnerText = claimedRole });
            }
        }

        foreach (var signatureCommitment in parameters.SignatureCommitments)
        {
            var cti = new CommitmentTypeIndication();
            cti.CommitmentTypeId.Identifier.IdentifierUri = signatureCommitment.CommitmentType.URI;
            cti.AllSignedDataObjects = true;

            foreach (var signatureCommitmentQualifier in signatureCommitment.CommitmentTypeQualifiers)
            {
                var ctq = new CommitmentTypeQualifier();
                ctq.AnyXmlElement = signatureCommitmentQualifier;

                cti.CommitmentTypeQualifiers.CommitmentTypeQualifierCollection.Add(ctq);
            }

            signedDataObjectProperties.CommitmentTypeIndicationCollection.Add(cti);
        }

        if (parameters.SignatureProductionPlace is not null)
        {
            signedSignatureProperties.SignatureProductionPlace.City = parameters.SignatureProductionPlace.City;
            signedSignatureProperties.SignatureProductionPlace.StateOrProvince = parameters.SignatureProductionPlace.StateOrProvince;
            signedSignatureProperties.SignatureProductionPlace.PostalCode = parameters.SignatureProductionPlace.PostalCode;
            signedSignatureProperties.SignatureProductionPlace.CountryName = parameters.SignatureProductionPlace.CountryName;
        }
    }
}