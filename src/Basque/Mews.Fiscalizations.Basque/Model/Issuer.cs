using FuncSharp;
using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Basque.Model
{
    public sealed class Issuer
    {
        private Issuer(TaxpayerIdentificationNumber nif, Name name)
        {
            Nif = Check.IsNotNull(nif, nameof(nif));
            Name = Check.IsNotNull(name, nameof(name));
        }

        public TaxpayerIdentificationNumber Nif { get; }

        public Name Name { get; }

        public static ITry<Issuer, Error> Create(Name name, string nif)
        {
            return TaxpayerIdentificationNumber.Create(Countries.Spain, nif, isCountryCodePrefixAllowed: false).Map(n => new Issuer(n, name));
        }
    }
}