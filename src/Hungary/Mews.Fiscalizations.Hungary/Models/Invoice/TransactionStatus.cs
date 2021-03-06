using Mews.Fiscalizations.Core.Model;
using System.Collections.Generic;

namespace Mews.Fiscalizations.Hungary.Models
{
    public sealed class TransactionStatus
    {
        public TransactionStatus(IEnumerable<Indexed<InvoiceStatus>> invoiceStatuses)
        {
            InvoiceStatuses = invoiceStatuses.AsList();
        }

        public List<Indexed<InvoiceStatus>> InvoiceStatuses { get; }
    }
}