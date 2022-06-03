using FuncSharp;
using System;

namespace Mews.Fiscalizations.TicketBai.Model
{
    public sealed class PreviousInvoiceChain
    {
        public PreviousInvoiceChain(String1To20 number, DateTime issueDate, String1To100 signature, String1To20 series = null)
        {
            Number = number;
            IssueDate = issueDate;
            Signature = signature;
            Series = series.ToOption();
        }

        public String1To20 Number { get; }

        public DateTime IssueDate { get; }

        public String1To100 Signature { get; }

        public IOption<String1To20> Series { get; }
    }
}
