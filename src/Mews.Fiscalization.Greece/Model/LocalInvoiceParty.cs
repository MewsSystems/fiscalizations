using FuncSharp;
using Mews.Fiscalization.Core.Model;

namespace Mews.Fiscalization.Greece.Model
{
    public sealed class LocalInvoiceParty
    {
        private LocalInvoiceParty(InvoicePartyInfo info, Country country)
        {
            Info = info;
            Country = country;
        }

        public InvoicePartyInfo Info { get; }

        public Country Country { get; }

        public static ITry<LocalInvoiceParty, INonEmptyEnumerable<Error>> Create(InvoicePartyInfo info)
        {
            return ObjectValidations.NotNull(info).Map(i => new LocalInvoiceParty(i, Countries.Greece));
        }
    }
}
