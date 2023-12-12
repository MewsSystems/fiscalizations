using Mews.Fiscalizations.Core.Xml.Signing.Crypto;

namespace Mews.Fiscalizations.Core.Xml.Signing.Signature.Parameters;

public sealed class SignaturePolicyInfo(
    string policyIdentifier = null,
    string policyHash = null,
    DigestMethod policyDigestAlgorithm = null,
    string policyUri = null)
{
    public string PolicyIdentifier { get; } = policyIdentifier;

    public string PolicyHash { get; } = policyHash;

    public DigestMethod PolicyDigestAlgorithm { get; } = policyDigestAlgorithm ?? DigestMethod.SHA1;

    public string PolicyUri { get; } = policyUri;
}