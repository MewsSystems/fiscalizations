using System.Xml;

namespace Mews.Fiscalizations.Basque.Model
{
    public sealed class TicketBaiInvoiceData
    {
        public TicketBaiInvoiceData(XmlDocument signedRequest, string tbaiIdentifier, string qrCodeUri)
        {
            SignedRequest = signedRequest;
            TbaiIdentifier = tbaiIdentifier;
            QrCodeUri = qrCodeUri;
        }

        public XmlDocument SignedRequest { get; }

        public string TbaiIdentifier { get; }

        public string QrCodeUri { get; }
    }
}
