using FuncSharp;
using Mews.Fiscalizations.Core.Model;
using System;

namespace Mews.Fiscalizations.Basque.Model;

public sealed class OriginalInvoiceInfo
{
    public OriginalInvoiceInfo(String1To20 number, DateTime issueDate, String1To100 signature, String1To20 series = null)
    {
        Number = Check.IsNotNull(number, nameof(number));
        IssueDate = Check.IsNotNull(issueDate, nameof(issueDate));
        Signature = Check.IsNotNull(signature, nameof(signature));
        Series = series.ToOption();
    }

    public String1To20 Number { get; }

    public DateTime IssueDate { get; }

    public String1To100 Signature { get; }

    public IOption<String1To20> Series { get; }
}