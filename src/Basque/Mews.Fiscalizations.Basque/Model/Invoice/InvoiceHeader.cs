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
            DateTime issued,
            bool? isSimplified = null,
            bool? issuedInSubstitutionOfSimplifiedInvoice = null,
            String1To20 series = null,
            CorrectingInvoice correctingInvoice = null,
            IEnumerable<CorrectedInvoice> correctedInvoices = null)
        {
            Number = number;
            Issued = issued;
            IsSimplified = isSimplified.ToOption();
            IssuedInSubstitutionOfSimplifiedInvoice = issuedInSubstitutionOfSimplifiedInvoice.ToOption();
            Series = series.ToOption();
            CorrectingInvoice = correctingInvoice.ToOption();
            CorrectedInvoices = correctedInvoices.ToOption();
            CorrectedInvoices.Match(c => Check.Condition(c.Count() <= 1000, "[1, 1000] corrected invoices."));
        }

        public String1To20 Number { get; }

        public DateTime Issued { get; }

        public IOption<bool> IsSimplified { get; }

        public IOption<bool> IssuedInSubstitutionOfSimplifiedInvoice { get; }

        public IOption<String1To20> Series { get; }

        public IOption<CorrectingInvoice> CorrectingInvoice { get; }

        public IOption<IEnumerable<CorrectedInvoice>> CorrectedInvoices { get; }
    }
}