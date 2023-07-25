using Mews.Fiscalizations.Core.Model;
using System;

namespace Mews.Fiscalizations.Basque.Model;

public sealed class CorrectedInvoice
{
    public CorrectedInvoice(String1To20 series, String1To20 number, DateTime issueDate)
    {
        Series = series;
        Number = number;
        IssueDate = issueDate;
    }

    public String1To20 Series { get; }

    public String1To20 Number { get; }

    public DateTime IssueDate { get; }
}