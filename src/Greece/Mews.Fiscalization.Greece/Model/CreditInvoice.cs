using FuncSharp;
using Mews.Fiscalization.Core.Model;
using System.Collections.Generic;

namespace Mews.Fiscalization.Greece.Model
{
    public sealed class CreditInvoice
    {
        private CreditInvoice(
            InvoiceInfo info,
            ISequenceStartingWithOne<NegativeRevenue> revenueItems,
            INonEmptyEnumerable<NegativePayment> payments,
            InvoiceParty counterpart,
            long? correlatedInvoice = null)
        {
            Info = info;
            RevenueItems = revenueItems;
            Payments = payments;
            Counterpart = counterpart;
            CorrelatedInvoice = correlatedInvoice.ToOption();
        }

        public InvoiceInfo Info { get; }

        public ISequenceStartingWithOne<NegativeRevenue> RevenueItems { get; }

        public INonEmptyEnumerable<NegativePayment> Payments { get; }

        public InvoiceParty Counterpart { get; }

        public IOption<long> CorrelatedInvoice { get; }

        public static ITry<CreditInvoice, IEnumerable<Error>> Create(
            InvoiceInfo info,
            ISequenceStartingWithOne<NegativeRevenue> revenueItems,
            INonEmptyEnumerable<NegativePayment> payments,
            InvoiceParty counterPart,
            long? correlatedInvoice = null)
        {
            return Try.Aggregate(
                ObjectValidations.NotNull(info),
                ObjectValidations.NotNull(revenueItems),
                ObjectValidations.NotNull(payments),
                ObjectValidations.NotNull(counterPart),
                (i, r, p, c) => new CreditInvoice(i, r, p, c, correlatedInvoice)
            );
        }
    }
}
