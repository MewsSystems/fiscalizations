using Mews.Fiscalization.Greece.Model.Types;
using System;
using Mews.Fiscalization.Core.Model;

namespace Mews.Fiscalization.Greece.Model
{
    public class InvoiceHeader
    {
        public InvoiceHeader(
            String1To50 invoiceSeries,
            String1To50 invoiceSerialNumber,
            DateTime invoiceIssueDate,
            string invoiceIdentifier = null,
            CurrencyCode currencyCode = null,
            ExchangeRate exchangeRate = null)
        {
            InvoiceSeries = Check.IsNotNull(invoiceSeries, nameof(invoiceSeries));
            InvoiceSerialNumber = Check.IsNotNull(invoiceSerialNumber, nameof(invoiceSerialNumber));
            InvoiceIssueDate = invoiceIssueDate;
            InvoiceIdentifier = invoiceIdentifier;
            CurrencyCode = currencyCode;
            ExchangeRate = exchangeRate;
        }

        public String1To50 InvoiceSeries { get; }

        public String1To50 InvoiceSerialNumber { get; }

        public DateTime InvoiceIssueDate { get; }

        public string InvoiceIdentifier { get; }

        public CurrencyCode CurrencyCode { get; }

        public ExchangeRate ExchangeRate { get; }
    }
}
