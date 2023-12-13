using System.Security.Cryptography.X509Certificates;

namespace Mews.Fiscalizations.Core.Xml.Signing.Signature.Parameters;

public sealed class SignerRole(IEnumerable<X509Certificate2> certifiedRoles = null, IEnumerable<string> claimedRoles = null)
{
    public IReadOnlyList<X509Certificate2> CertifiedRoles { get; } = certifiedRoles?.ToReadOnlyList() ?? [];

    public IReadOnlyList<string> ClaimedRoles { get; } = claimedRoles?.ToReadOnlyList() ?? [];
}