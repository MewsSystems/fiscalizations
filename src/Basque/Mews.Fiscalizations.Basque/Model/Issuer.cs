using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Basque.Model
{
    public sealed class Issuer
    {
        public Issuer(TaxpayerIdentificationNumber nif, Name name)
        {
            Nif = Check.IsNotNull(nif, nameof(nif));
            Name = Check.IsNotNull(name, nameof(name));
        }

        public TaxpayerIdentificationNumber Nif { get; }

        public Name Name { get; }
    }
}