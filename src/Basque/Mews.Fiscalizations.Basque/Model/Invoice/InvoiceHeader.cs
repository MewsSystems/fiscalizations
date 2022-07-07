using FuncSharp;
using Mews.Fiscalizations.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalizations.Basque.Model
{
    public sealed class InvoiceHeader
    {
        public InvoiceHeader(
            String1To20 number,
            DateTime issueDate,
            DateTime issueDateTime,
            bool isSimplified = false,
            bool issuedInSubstitutionOfSimplifiedInvoice = false,
            String1To20 series = null,
            CorrectingInvoice correctingInvoice = null,
            IEnumerable<CorrectedInvoice> correctedInvoices = null)
        {
            Number = number;
            IssueDate = issueDate;
            IssueDateTime = issueDateTime;
            IsSimplified = isSimplified;
            IssuedInSubstitutionOfSimplifiedInvoice = issuedInSubstitutionOfSimplifiedInvoice;
            Series = series.ToOption();
            CorrectingInvoice = correctingInvoice.ToOption();
            CorrectedInvoices = correctedInvoices.ToOption();
            CorrectedInvoices.Match(c => Check.Condition(c.Count() <= 1000, "[1, 1000] corrected invoices."));
        }

        public String1To20 Number { get; }

        public DateTime IssueDate { get; }

        public DateTime IssueDateTime { get; }

        public bool IsSimplified { get; }

        public bool IssuedInSubstitutionOfSimplifiedInvoice { get; }

        public IOption<String1To20> Series { get; }

        public IOption<CorrectingInvoice> CorrectingInvoice { get; }

        public IOption<IEnumerable<CorrectedInvoice>> CorrectedInvoices { get; }
    }
}