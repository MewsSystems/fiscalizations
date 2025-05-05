using Mews.Fiscalizations.Fiskaly.DTOs.SignES;
using Mews.Fiscalizations.Fiskaly.DTOs.SignES.Signers;
using Mews.Fiscalizations.Fiskaly.Models.SignES.Signers;
using SignerCertificate = Mews.Fiscalizations.Fiskaly.Models.SignES.Signers.SignerCertificate;

namespace Mews.Fiscalizations.Fiskaly.Mappers.SignES.Signers;

internal static class SignerMapper
{
    public static Signer MapSignerResponse(this ContentWrapper<SignerDataResponse> response)
    {
        var cert = response.Content.Certificate;
        return new Signer(
            Id: response.Content.Id,
            Certificate: new SignerCertificate(cert.SerialNumber, cert.X509Pem, cert.ExpiresAt)
        );
    }
}