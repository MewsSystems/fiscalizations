using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;

namespace Mews.Fiscalizations.Czechia.Tests;

/// <summary>
/// Self-signed certificates generated in memory, used by the tests that don't talk to the playground.
/// </summary>
internal static class TestCertificates
{
    public static (X509Certificate2 Certificate, RSA Key) CreateSelfSigned(string subject = "CN=CZ00000019, C=CZ")
    {
        var key = RSA.Create(2048);
        var name = new X500DistinguishedName(subject);
        var request = new CertificateRequest(name, key, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        var certificate = request.Create(
            issuerName: name,
            generator: X509SignatureGenerator.CreateForRSA(key, RSASignaturePadding.Pkcs1),
            notBefore: DateTimeOffset.UtcNow.AddDays(-1),
            notAfter: DateTimeOffset.UtcNow.AddDays(1),
            serialNumber: RandomNumberGenerator.GetBytes(8)
        );
        return (certificate, key);
    }

    public static Certificate Create()
    {
        var (certificate, key) = CreateSelfSigned();
        return Certificate.Create(certificate, key).GetUnsafe();
    }

    public static byte[] CreatePkcs12(X509Certificate2 certificate, RSA key, string password)
    {
        var encryption = new PbeParameters(PbeEncryptionAlgorithm.Aes256Cbc, HashAlgorithmName.SHA256, iterationCount: 2048);
        var certificates = new Pkcs12SafeContents();
        certificates.AddCertificate(certificate);
        var keys = new Pkcs12SafeContents();
        keys.AddShroudedKey(key, password, encryption);

        var builder = new Pkcs12Builder();
        builder.AddSafeContentsEncrypted(certificates, password, encryption);
        builder.AddSafeContentsUnencrypted(keys);
        builder.SealWithMac(password, HashAlgorithmName.SHA256, iterationCount: 2048);
        return builder.Encode();
    }
}
