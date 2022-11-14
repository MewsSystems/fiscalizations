namespace Mews.Fiscalizations.Basque.Model
{
    public sealed class TbaiInvoiceData
    {
        public TbaiInvoiceData(string tbaiIdentifier, string qrCodeUri)
        {
            TbaiIdentifier = tbaiIdentifier;
            QrCodeUri = qrCodeUri;
        }

        public string TbaiIdentifier { get; }

        public string QrCodeUri { get; }
    }
}
