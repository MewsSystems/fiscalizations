using FuncSharp;
using Mews.Fiscalizations.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalizations.TicketBai.Model
{
    public sealed class InvoiceData
    {
        public InvoiceData(
            String1To250 descripton,
            IEnumerable<InvoiceItem> items,
            decimal totalAmount,
            IEnumerable<TaxRegulationType> taxRegulationTypes,
            decimal? supportWithheldAmount = null,
            decimal? tax = null,
            DateTime? transactionDate = null)
        {
            Descripton = descripton;

            Check.Condition(items.Count() <= 1000, "[1, 1000] items."); //TODO: ITry.
            Items = items;
            TotalAmount = totalAmount;
            TaxRegulationTypes = taxRegulationTypes;
            SupportWithheldAmount = supportWithheldAmount.ToOption();
            Tax = tax.ToOption();
            TransactionDate = transactionDate.ToOption();
        }

        public String1To250 Descripton { get; }

        public IEnumerable<InvoiceItem> Items { get; }

        public decimal TotalAmount { get; }

        public IEnumerable<TaxRegulationType> TaxRegulationTypes { get; }

        public IOption<decimal> SupportWithheldAmount { get; }

        public IOption<decimal> Tax { get; }

        public IOption<DateTime> TransactionDate { get; }
    }
}
