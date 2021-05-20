using FuncSharp;
using Mews.Fiscalization.Core.Model;

namespace Mews.Fiscalization.Greece.Model
{
    public sealed class ForeignInvoiceParty
    {
        private ForeignInvoiceParty(InvoicePartyInfo info, Country country)
        {
            Country = country;
            Info = info;
        }

        public InvoicePartyInfo Info { get; }

        public Country Country { get; }

        public static ITry<ForeignInvoiceParty, INonEmptyEnumerable<Error>> Create(InvoicePartyInfo info, Country country)
        {
            return ObjectValidations.NotNull(info).FlatMap(i =>
            {
                var validCountry = country.ToTry(c => c != Countries.Greece, _ => Error.Create($"{nameof(ForeignInvoiceParty)} cannot use greece as a country.")) ;
                return validCountry.Map(c => new ForeignInvoiceParty(i, c));
            });
        }
    }
}
