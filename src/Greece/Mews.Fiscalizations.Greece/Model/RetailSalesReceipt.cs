using FuncSharp;
using Mews.Fiscalizations.Core.Model;
using System.Collections.Generic;

namespace Mews.Fiscalizations.Greece.Model
{
    public sealed class RetailSalesReceipt
    {
        private RetailSalesReceipt(InvoiceInfo info, ISequenceStartingWithOne<NonNegativeRevenue> revenueItems, INonEmptyEnumerable<NonNegativePayment> payments)
        {
            Info = info;
            RevenueItems = revenueItems;
            Payments = payments;
        }

        public InvoiceInfo Info { get; }

        public ISequenceStartingWithOne<NonNegativeRevenue> RevenueItems { get; }

        public INonEmptyEnumerable<NonNegativePayment> Payments { get; }

        public static ITry<RetailSalesReceipt, IEnumerable<Error>> Create(InvoiceInfo info, ISequenceStartingWithOne<NonNegativeRevenue> revenueItems, INonEmptyEnumerable<NonNegativePayment> payments)
        {
            return Try.Aggregate(
                ObjectValidations.NotNull(info),
                ObjectValidations.NotNull(revenueItems),
                ObjectValidations.NotNull(payments),
                (i, r, p) => new RetailSalesReceipt(i, r, p)
            );
        }
    }
}
