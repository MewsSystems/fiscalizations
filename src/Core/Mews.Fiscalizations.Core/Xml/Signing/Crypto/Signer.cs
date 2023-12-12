using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Mews.Fiscalizations.Core.Xml.Signing.Crypto;

public sealed class Signer
{
    public X509Certificate2 Certificate { get; }

    public AsymmetricAlgorithm SigningKey { get; private set; }

    public Signer(X509Certificate2 certificate)
    {
        if (!certificate.HasPrivateKey)
        {
            throw new Exception("The certificate does not contain any private key.");
        }

        Certificate = certificate;
        SetSigningKey(Certificate);
    }

    private void SetSigningKey(X509Certificate2 certificate)
    {
        SigningKey = certificate.GetRSAPrivateKey();
    }
}