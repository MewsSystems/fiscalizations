using FuncSharp;
using Mews.Fiscalization.Core.Model;

namespace Mews.Fiscalization.Greece.Model
{
    public sealed class InvoiceInfo
    {
        private InvoiceInfo(InvoiceHeader header, InvoiceParty issuer)
        {
            Header = header;
            Issuer = issuer;
        }

        public InvoiceHeader Header { get; }

        public InvoiceParty Issuer { get; }

        public static ITry<InvoiceInfo, INonEmptyEnumerable<Error>> Create(InvoiceHeader header, InvoiceParty issuer)
        {
            return ObjectValidations.NotNull(header).FlatMap(h =>
            {
                var validIssuer = ObjectValidations.NotNull(issuer);
                return validIssuer.Map(i => new InvoiceInfo(h, i));
            });
        }
    }
}
