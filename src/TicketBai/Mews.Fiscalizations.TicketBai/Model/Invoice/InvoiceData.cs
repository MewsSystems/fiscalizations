using FuncSharp;
using Mews.Fiscalizations.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalizations.TicketBai.Model
{
    public sealed class InvoiceData
    {
        private InvoiceData(
            String1To250 description,
            IEnumerable<InvoiceItem> items,
            decimal totalAmount,
            IEnumerable<TaxMode> taxModes,
            decimal? supportWithheldAmount = null,
            decimal? tax = null,
            DateTime? transactionDate = null)
        {
            Description = description;
            Items = items;
            TotalAmount = totalAmount;
            TaxModes = taxModes;
            SupportWithheldAmount = supportWithheldAmount.ToOption();
            Tax = tax.ToOption();
            TransactionDate = transactionDate.ToOption();
        }

        public String1To250 Description { get; }

        public IEnumerable<InvoiceItem> Items { get; }

        public decimal TotalAmount { get; }

        public IEnumerable<TaxMode> TaxModes { get; }

        public IOption<decimal> SupportWithheldAmount { get; }

        public IOption<decimal> Tax { get; }

        public IOption<DateTime> TransactionDate { get; }

        public ITry<InvoiceData, IEnumerable<Error>> Create(
            String1To250 description,
            IEnumerable<InvoiceItem> items,
            decimal totalAmount,
            IEnumerable<TaxMode> taxModes,
            decimal? supportWithheldAmount = null,
            decimal? tax = null,
            DateTime? transactionDate = null)
        {
            return Try.Aggregate(
                ObjectValidations.NotNull(description),
                items.ToTry(i => i.Count() >= 1 && i.Count() <= 1000, _ => new Error($"{nameof(items)} count must be in range [1, 1000].")),
                ObjectValidations.NotNull(totalAmount),
                taxModes.ToTry(t => t.NonEmpty(), _ => new Error($"{nameof(taxModes)} shouldn't be empty.")),
                (d, i, a, t) => new InvoiceData(d, i, a, t, supportWithheldAmount, tax, transactionDate)
            );
        }
    }
}