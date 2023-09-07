using Mews.Fiscalizations.Austria.Dto;

namespace Mews.Fiscalizations.Austria;

public interface ISigner
{
    Task<SignerOutput> SignAsync(QrData qrData);
}