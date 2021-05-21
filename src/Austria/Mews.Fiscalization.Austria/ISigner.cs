using Mews.Fiscalizations.Austria.Dto;

namespace Mews.Fiscalizations.Austria
{
    public interface ISigner
    {
        SignerOutput Sign(QrData qrData);
    }
}
