using FuncSharp;
using Mews.Fiscalization.Core.Model;

namespace Mews.Fiscalization.Greece.Model
{
    public sealed class Invoice : Coproduct4<SalesInvoice, SimplifiedInvoice, RetailSalesReceipt, CreditInvoice>
    {
        public Invoice(SalesInvoice salesInvoice)
            : base(salesInvoice)
        {
        }

        public Invoice(SimplifiedInvoice simplifiedInvoice)
            : base(simplifiedInvoice)
        {
        }

        public Invoice(RetailSalesReceipt retailSalesReceipt)
            : base(retailSalesReceipt)
        {
        }

        public Invoice(CreditInvoice creditInvoice)
            : base(creditInvoice)
        {
        }

        public InvoiceInfo Info
        {
            get
            {
                return Match(
                    salesInvoice => salesInvoice.Info,
                    simplifiedInvoice => simplifiedInvoice.Info,
                    retailSalesReceipt => retailSalesReceipt.Info,
                    creditInvoice => creditInvoice.Info
                );
            }
        }

        public IOption<InvoiceParty> Counterpart
        {
            get
            {
                return Match(
                    salesInvoice => salesInvoice.Counterpart.ToOption(),
                    simplifiedInvoice => Option.Empty<InvoiceParty>(),
                    retailSalesReceipt => Option.Empty<InvoiceParty>(),
                    creditInvoice => creditInvoice.Counterpart.ToOption()
                );
            }
        }

        public INonEmptyEnumerable<Payment> Payments
        {
            get
            {
                return Match(
                    salesInvoice => salesInvoice.Payments.Select(p => new Payment(p)),
                    simplifiedInvoice => simplifiedInvoice.Payments.Select(p => new Payment(p)),
                    retailSalesReceipt => retailSalesReceipt.Payments.Select(p => new Payment(p)),
                    creditInvoice => creditInvoice.Payments.Select(p => new Payment(p))
                );
            }
        }

        public IOption<long> CorrelatedInvoice
        {
            get { return Fourth.FlatMap(creditInvoice => creditInvoice.CorrelatedInvoice); }
        }

        public ISequenceStartingWithOne<Revenue> RevenueItems
        {
            get
            {
                return Match(
                    salesInvoice => SequenceStartingWithOne.FromPreordered(salesInvoice.RevenueItems.Values.Select(r => new Revenue(r.Value))),
                    simplifiedInvoice => SequenceStartingWithOne.FromPreordered(simplifiedInvoice.RevenueItems.Values.Select(r => new Revenue(r.Value))),
                    retailSalesReceipt => SequenceStartingWithOne.FromPreordered(retailSalesReceipt.RevenueItems.Values.Select(r => new Revenue(r.Value))),
                    creditInvoice => SequenceStartingWithOne.FromPreordered(creditInvoice.RevenueItems.Values.Select(r => new Revenue(r.Value)))
                );
            }
        }
    }
}
