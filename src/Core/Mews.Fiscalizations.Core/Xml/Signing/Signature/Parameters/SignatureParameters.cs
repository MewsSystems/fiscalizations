using System.Xml;
using Mews.Fiscalizations.Core.Xml.Signing.Crypto;

namespace Mews.Fiscalizations.Core.Xml.Signing.Signature.Parameters;
   
public sealed class SignatureParameters(
    XmlDocument xmlDocumentToSign,
    Signer signer,
    SignatureMethod signatureMethod,
    DigestMethod digestMethod,
    DateTime signingDate,
    SignerRole signerRole = null,
    List<SignatureCommitment> signatureCommitments = null,
    SignatureProductionPlace signatureProductionPlace = null,
    List<SignatureXPathExpression> xPathTransformations = null,
    SignaturePolicyInfo signaturePolicyInfo = null,
    SignatureXPathExpression signatureDestination = null,
    DataFormat dataFormat = null,
    string elementIdToSign = null,
    string externalContentUri = null)
{
    public XmlDocument XmlDocumentToSign { get; } = xmlDocumentToSign;

    public Signer Signer { get; } = signer;

    public SignatureMethod SignatureMethod { get; } = signatureMethod;

    public DigestMethod DigestMethod { get; } = digestMethod;

    public DateTime SigningDate { get; } = signingDate;

    public SignerRole SignerRole { get; } = signerRole;

    public List<SignatureCommitment> SignatureCommitments { get; } = signatureCommitments ?? [];

    public SignatureProductionPlace SignatureProductionPlace { get; } = signatureProductionPlace;

    public List<SignatureXPathExpression> XPathTransformations { get; } = xPathTransformations ?? [];

    public SignaturePolicyInfo SignaturePolicyInfo { get; } = signaturePolicyInfo;

    public SignatureXPathExpression SignatureDestination { get; } = signatureDestination;

    public DataFormat DataFormat { get; } = dataFormat;

    public string ElementIdToSign { get; } = elementIdToSign;

    public string ExternalContentUri { get; } = externalContentUri;
}