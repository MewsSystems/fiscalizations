using FuncSharp;

namespace Mews.Fiscalizations.TicketBai.Model
{
    public sealed class InvoiceFooter
    {
        public InvoiceFooter(Software software, PreviousInvoiceChain previousInvoiceChain = null, String1To30 deviceSerialNumber = null)
        {
            Software = software;
            PreviousInvoiceChain = previousInvoiceChain.ToOption();
            DeviceSerialNumber = deviceSerialNumber.ToOption();
        }

        public Software Software { get; }

        public IOption<PreviousInvoiceChain> PreviousInvoiceChain { get; }

        public IOption<String1To30> DeviceSerialNumber { get; }
    }
}
