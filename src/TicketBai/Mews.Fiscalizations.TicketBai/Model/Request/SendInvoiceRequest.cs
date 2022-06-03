using FuncSharp;

namespace Mews.Fiscalizations.TicketBai.Model
{
    public sealed class SendInvoiceRequest
    {
        public SendInvoiceRequest(Subject subject, Invoice invoice, InvoiceFooter invoiceFooter, Signature signature = null)
        {
            Subject = subject;
            Invoice = invoice;
            InvoiceFooter = invoiceFooter;
            Signature = signature.ToOption();
        }

        public Subject Subject { get; }

        public Invoice Invoice { get; }

        public InvoiceFooter InvoiceFooter { get; }

        public IOption<Signature> Signature { get; }
    }
}
