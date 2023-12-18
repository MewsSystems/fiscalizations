using Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

namespace Mews.Fiscalizations.Core.Xml.Signing.Utils;

internal static class DigestUtil
{
    public static void SetCertDigest(byte[] rawCert,  Mews.Fiscalizations.Core.Xml.Signing.Crypto.DigestMethod digestMethod, DigestAlgAndValueType destination)
    {
        using var hashAlg = digestMethod.GetHashAlgorithm();
        destination.DigestMethod.Algorithm = digestMethod.URI;
        destination.DigestValue = hashAlg.ComputeHash(rawCert);
    }
}