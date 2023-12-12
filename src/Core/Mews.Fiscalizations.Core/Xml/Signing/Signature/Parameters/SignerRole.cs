using System.Security.Cryptography.X509Certificates;

namespace Mews.Fiscalizations.Core.Xml.Signing.Signature.Parameters;

public sealed class SignerRole(List<X509Certificate> certifiedRoles = null, List<string> claimedRoles = null)
{
    public List<X509Certificate> CertifiedRoles { get; } = certifiedRoles ?? [];

    public List<string> ClaimedRoles { get; } = claimedRoles ?? [];
}