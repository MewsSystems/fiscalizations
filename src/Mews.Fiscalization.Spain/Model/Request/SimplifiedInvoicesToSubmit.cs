using FuncSharp;
using Mews.Fiscalization.Core.Model;
using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalization.Spain.Model.Request
{
    public sealed class SimplifiedInvoicesToSubmit
    {
        private SimplifiedInvoicesToSubmit(Header header, SimplifiedInvoice[] invoices)
        {
            Header = header;
            Invoices = invoices;
        }

        public Header Header { get; }

        public SimplifiedInvoice[] Invoices { get; }

        public static ITry<SimplifiedInvoicesToSubmit, IEnumerable<Error>> Create(Header header, SimplifiedInvoice[] invoices)
        {
            return Try.Aggregate(
                ObjectValidations.NotNull(header),
                invoices.OrEmptyIfNull().ToTry(i => i.Count() >= 1 && i.Count() <= 10000, _ => Error.Create($"{nameof(invoices)} count must be in range [1, 10000].")),
                (h, i) => new SimplifiedInvoicesToSubmit(h, i.ToArray())
            );
        }
    }
}
