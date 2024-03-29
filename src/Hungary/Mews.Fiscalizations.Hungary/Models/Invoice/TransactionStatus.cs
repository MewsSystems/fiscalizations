namespace Mews.Fiscalizations.Hungary.Models;

public sealed class TransactionStatus
{
    public TransactionStatus(IEnumerable<Indexed<InvoiceStatus>> invoiceStatuses)
    {
        InvoiceStatuses = invoiceStatuses.ToList();
    }

    public List<Indexed<InvoiceStatus>> InvoiceStatuses { get; }
}