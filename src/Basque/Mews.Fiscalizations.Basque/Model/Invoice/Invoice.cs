namespace Mews.Fiscalizations.Basque.Model
{
    public sealed class Invoice
    {
        public Invoice(InvoiceHeader header, InvoiceData invoiceData, TaxBreakdown taxBreakdown)
        {
            Header = header;
            InvoiceData = invoiceData;
            TaxBreakdown = taxBreakdown;
        }

        public InvoiceHeader Header { get; }

        public InvoiceData InvoiceData { get; }

        public TaxBreakdown TaxBreakdown { get; }
    }
}