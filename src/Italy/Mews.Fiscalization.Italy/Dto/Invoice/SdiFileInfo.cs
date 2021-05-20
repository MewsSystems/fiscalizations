using System;

namespace Mews.Fiscalization.Italy.Dto.Invoice
{
    public class SdiFileInfo
    {
        public SdiFileInfo(DateTime receivedUtc, string sdiIdentifier)
        {
            ReceivedUtc = receivedUtc;
            SdiIdentifier = sdiIdentifier;
        }

        public DateTime ReceivedUtc { get; }

        public string SdiIdentifier { get; }
    }
}