namespace Mews.Fiscalization.Italy.Dto.Invoice
{
    public class SdiResponse
    {
        public SdiResponse(SdiFileInfo sdiFIleInfo)
        {
            SdiFIleInfo = sdiFIleInfo;
        }

        public SdiResponse(SdiError error)
        {
            Error = error;
        }

        public SdiFileInfo SdiFIleInfo { get; }

        public SdiError? Error { get; }

        public bool IsError => Error != null;

        public bool IsSucces => !IsError;
    }
}