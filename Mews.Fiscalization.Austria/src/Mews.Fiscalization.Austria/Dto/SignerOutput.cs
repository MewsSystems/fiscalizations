using Mews.Fiscalization.Austria.Dto.Identifiers;

namespace Mews.Fiscalization.Austria.Dto
{
    public sealed class SignerOutput
    {
        public SignerOutput(JwsRepresentation jwsRepresentation, QrData qrData = null)
        {
            JwsRepresentation = jwsRepresentation;
            if (qrData != null)
            {
                SignedQrData = new SignedQrData(qrData, jwsRepresentation.Signature);
            }
        }

        public JwsRepresentation JwsRepresentation { get; }

        public SignedQrData SignedQrData { get; }
    }
}
