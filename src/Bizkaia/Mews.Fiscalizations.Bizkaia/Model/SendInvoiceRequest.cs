namespace Mews.Fiscalizations.Bizkaia.Model;

public sealed class SendInvoiceRequest
{
    public SendInvoiceRequest(Subject subject, Invoice invoice, InvoiceFooter invoiceFooter)
    {
        Subject = subject;
        Invoice = invoice;
        InvoiceFooter = invoiceFooter;
    }

    public Subject Subject { get; }

    public Invoice Invoice { get; }

    public InvoiceFooter InvoiceFooter { get; }
}