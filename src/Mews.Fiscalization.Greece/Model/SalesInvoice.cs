using FuncSharp;
using Mews.Fiscalization.Core.Model;
using System.Collections.Generic;

namespace Mews.Fiscalization.Greece.Model
{
    public sealed class SalesInvoice
    {
        private SalesInvoice(
            InvoiceInfo info,
            ISequenceStartingWithOne<NonNegativeRevenue> revenueItems,
            INonEmptyEnumerable<NonNegativePayment> payments,
            InvoiceParty counterpart)
        {
            Info = info;
            RevenueItems = revenueItems;
            Payments = payments;
            Counterpart = counterpart;
        }

        public InvoiceInfo Info { get; }

        public ISequenceStartingWithOne<NonNegativeRevenue> RevenueItems { get; }

        public INonEmptyEnumerable<NonNegativePayment> Payments { get; }

        public InvoiceParty Counterpart { get; }

        public static ITry<SalesInvoice, IEnumerable<Error>> Create(
            InvoiceInfo info,
            ISequenceStartingWithOne<NonNegativeRevenue> revenueItems,
            INonEmptyEnumerable<NonNegativePayment> payments,
            InvoiceParty counterpart)
        {
            return Try.Aggregate(
                ObjectValidations.NotNull(info),
                ObjectValidations.NotNull(revenueItems),
                ObjectValidations.NotNull(payments),
                ObjectValidations.NotNull(counterpart),
                (i, r, p, c) => new SalesInvoice(i, r, p, c)
            );
        }
    }
}
