﻿namespace Mews.Fiscalizations.Spain.Model.Request;

public sealed class SimplifiedInvoicesToSubmit
{
    private SimplifiedInvoicesToSubmit(Header header, SimplifiedInvoice[] invoices)
    {
        Header = header;
        Invoices = invoices;
    }

    public Header Header { get; }

    public SimplifiedInvoice[] Invoices { get; }

    public static Try<SimplifiedInvoicesToSubmit, IReadOnlyList<Error>> Create(Header header, SimplifiedInvoice[] invoices)
    {
        return Try.Aggregate(
            ObjectValidations.NotNull(header),
            invoices.OrEmptyIfNull().ToList().ToTry(i => i.Any() && i.Count <= 10000, _ => new Error($"{nameof(invoices)} count must be in range [1, 10000].")),
            (h, i) => new SimplifiedInvoicesToSubmit(h, i.ToArray())
        );
    }
}