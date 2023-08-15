using FuncSharp;
using Mews.Fiscalizations.Core.Model;
using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalizations.Spain.Model.Request;

public sealed class InvoicesToSubmit
{
    private InvoicesToSubmit(Header header, Invoice[] invoices)
    {
        Header = header;
        Invoices = invoices;
    }

    public Header Header { get; }

    public Invoice[] Invoices { get; }

    public static Try<InvoicesToSubmit, IReadOnlyList<Error>> Create(Header header, Invoice[] invoices)
    {
        return Try.Aggregate(
            ObjectValidations.NotNull(header),
            invoices.OrEmptyIfNull().ToList().ToTry(i => i.Any() && i.Count <= 10000, _ => new Error($"{nameof(invoices)} count must be in range [1, 10000].")),
            (h, i) => new InvoicesToSubmit(h, i.ToArray())
        );
    }
}