using FuncSharp;
using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.TicketBai.Model
{
    public sealed class InvoiceFooter
    {
        public InvoiceFooter(Software software, PreviousInvoiceChain previousInvoiceChain = null, String1To30 deviceSerialNumber = null)
        {
            Software = Check.IsNotNull(software, nameof(software));
            PreviousInvoiceChain = previousInvoiceChain.ToOption();
            DeviceSerialNumber = deviceSerialNumber.ToOption();
        }

        public Software Software { get; }

        public IOption<PreviousInvoiceChain> PreviousInvoiceChain { get; }

        public IOption<String1To30> DeviceSerialNumber { get; }
    }
}