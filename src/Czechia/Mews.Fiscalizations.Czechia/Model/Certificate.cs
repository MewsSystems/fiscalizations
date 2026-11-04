using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;

namespace Mews.Fiscalizations.Czechia.Model;

/// <summary>
/// Point of sale certificate issued by the EET certification authority, including its RSA private key.
/// </summary>
public sealed class Certificate
{
    private Certificate(X509Certificate2 x509Certificate, RSA privateKey)
    {
        X509Certificate = x509Certificate;
        PrivateKey = privateKey;
    }

    public X509Certificate2 X509Certificate { get; }

    internal RSA PrivateKey { get; }

    public static Try<Certificate, Error> Create(X509Certificate2 certificate)
    {
        if (certificate is null)
        {
            return Try.Error<Certificate, Error>(new Error("Certificate cannot be null."));
        }
        var privateKey = certificate.HasPrivateKey ? certificate.GetRSAPrivateKey() : null;
        if (privateKey is null)
        {
            return Try.Error<Certificate, Error>(new Error("Certificate must contain an RSA private key."));
        }
        return Try.Success<Certificate, Error>(new Certificate(certificate, privateKey));
    }

    /// <summary>
    /// Pairs a certificate with its private key, e.g. when the key lives in an HSM or a key vault.
    /// </summary>
    public static Try<Certificate, Error> Create(X509Certificate2 certificate, RSA privateKey)
    {
        if (certificate is null || privateKey is null)
        {
            return Try.Error<Certificate, Error>(new Error("Certificate and private key cannot be null."));
        }
        if (!HasSamePublicKey(certificate, privateKey))
        {
            return Try.Error<Certificate, Error>(new Error("Private key does not belong to the certificate."));
        }
        return Try.Success<Certificate, Error>(new Certificate(certificate, privateKey));
    }

    /// <summary>
    /// Loads the certificate from PKCS#12 (.p12/.pfx) data as distributed by the EET certification authority.
    /// The private key is kept in memory only, it is not imported into the user or machine key store (or the macOS keychain).
    /// </summary>
    public static Try<Certificate, Error> Create(byte[] pkcs12Data, string password)
    {
        try
        {
            var info = Pkcs12Info.Decode(pkcs12Data, out _, skipCopy: true);
            if (info.IntegrityMode == Pkcs12IntegrityMode.Password && !info.VerifyMac(password))
            {
                return Try.Error<Certificate, Error>(new Error("Certificate password is not valid."));
            }

            var certificates = new List<X509Certificate2>();
            var keys = new List<RSA>();
            foreach (var safeContents in info.AuthenticatedSafe)
            {
                if (safeContents.ConfidentialityMode == Pkcs12ConfidentialityMode.Password)
                {
                    safeContents.Decrypt(password);
                }
                foreach (var bag in safeContents.GetBags())
                {
                    switch (bag)
                    {
                        case Pkcs12CertBag { IsX509Certificate: true } certBag:
                            certificates.Add(certBag.GetCertificate());
                            break;
                        case Pkcs12ShroudedKeyBag keyBag:
                            var key = RSA.Create();
                            key.ImportEncryptedPkcs8PrivateKey(password, keyBag.EncryptedPkcs8PrivateKey.Span, out _);
                            keys.Add(key);
                            break;
                        case Pkcs12KeyBag keyBag:
                            var plainKey = RSA.Create();
                            plainKey.ImportPkcs8PrivateKey(keyBag.Pkcs8PrivateKey.Span, out _);
                            keys.Add(plainKey);
                            break;
                    }
                }
            }

            var pair = certificates
                .SelectMany(c => keys.Select(k => (Certificate: c, Key: k)))
                .FirstOrDefault(p => HasSamePublicKey(p.Certificate, p.Key));
            return pair.Certificate is null
                ? Try.Error<Certificate, Error>(new Error("PKCS#12 data does not contain a certificate with its RSA private key."))
                : Create(pair.Certificate, pair.Key);
        }
        catch (CryptographicException e)
        {
            return Try.Error<Certificate, Error>(new Error($"Certificate could not be loaded: {e.Message}"));
        }
    }

    public static Certificate CreateUnsafe(byte[] pkcs12Data, string password)
    {
        return Create(pkcs12Data, password).GetUnsafe();
    }

    private static bool HasSamePublicKey(X509Certificate2 certificate, RSA key)
    {
        using var publicKey = certificate.GetRSAPublicKey();
        return publicKey is not null && publicKey.ExportParameters(false).Modulus!.AsSpan().SequenceEqual(key.ExportParameters(false).Modulus);
    }
}
