using Mews.Fiscalizations.Fiskaly.DTOs.SignES.Signer;
using Mews.Fiscalizations.Fiskaly.Models.SignES.Signer;
using SignerCertificate = Mews.Fiscalizations.Fiskaly.Models.SignES.Signer.SignerCertificate;

namespace Mews.Fiscalizations.Fiskaly.Mappers.SignES.Signers;

internal static class SignerMapper
{
    public static Signer MapSignerResponse(this SignerResponse response)
    {
        var cert = response.SignerData.Certificate;
        return new Signer(
            Id: response.SignerData.Id,
            Certificate: new SignerCertificate(cert.SerialNumber, cert.X509Pem, cert.ExpiresAt)
        );
    }
}