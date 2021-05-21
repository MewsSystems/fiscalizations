using FuncSharp;
using Mews.Fiscalizations.Core.Model;
using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalizations.Spain.Model.Request
{
    public sealed class InvoicesToSubmit
    {
        private InvoicesToSubmit(Header header, Invoice[] invoices)
        {
            Header = header;
            Invoices = invoices;
        }

        public Header Header { get; }

        public Invoice[] Invoices { get; }

        public static ITry<InvoicesToSubmit, IEnumerable<Error>> Create(Header header, Invoice[] invoices)
        {
            return Try.Aggregate(
                ObjectValidations.NotNull(header),
                invoices.OrEmptyIfNull().ToTry(i => i.Count() >= 1 && i.Count() <= 10000, _ => Error.Create($"{nameof(invoices)} count must be in range [1, 10000].")),
                (h, i) => new InvoicesToSubmit(h, i.ToArray())
            );
        }
    }
}
