using Mews.Fiscalization.Austria.Dto;

namespace Mews.Fiscalization.Austria
{
    public interface ISigner
    {
        SignerOutput Sign(QrData qrData);
    }
}
