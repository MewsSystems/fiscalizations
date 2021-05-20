using System.Collections.Generic;
using System.Linq;
using FuncSharp;
using Mews.Fiscalization.Core.Model;

namespace Mews.Fiscalization.Greece.Model
{
    public sealed class SimplifiedInvoice
    {
        private SimplifiedInvoice(InvoiceInfo info, ISequenceStartingWithOne<NonNegativeRevenue> revenueItems, INonEmptyEnumerable<NonNegativePayment> payments)
        {
            Info = info;
            RevenueItems = revenueItems;
            Payments = payments;
        }

        public InvoiceInfo Info { get; }

        public ISequenceStartingWithOne<NonNegativeRevenue> RevenueItems { get; }

        public INonEmptyEnumerable<NonNegativePayment> Payments { get; }

        public static ITry<SimplifiedInvoice, IEnumerable<Error>> Create(InvoiceInfo info, ISequenceStartingWithOne<NonNegativeRevenue> revenueItems, INonEmptyEnumerable<NonNegativePayment> payments)
        {
            var result = Try.Aggregate(
                ObjectValidations.NotNull(info),
                ObjectValidations.NotNull(revenueItems),
                ObjectValidations.NotNull(payments),
                (i, r, p) => IsValidSimplifiedInvoice(i, r).ToTry(
                    t => new SimplifiedInvoice(i, r, p),
                    f => new Error($"{nameof(SimplifiedInvoice)} can only be below or equal to 100 EUR.").ToEnumerable()
                )
            );

            return result.FlatMap(r => r);
        }

        private static bool IsValidSimplifiedInvoice(InvoiceInfo info, ISequenceStartingWithOne<NonNegativeRevenue> revenueItems)
        {
            if (info.Header.CurrencyCode.IsNull() || info.Header.CurrencyCode.Value == "EUR")
            {
                return revenueItems.Values.Sum(i => i.Value.NetValue.Value + i.Value.VatValue.Value) <= 100;
            }
            return true;
        }
    }
}
