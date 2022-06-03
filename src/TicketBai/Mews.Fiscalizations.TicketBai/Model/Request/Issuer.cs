using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.TicketBai.Model
{
    public sealed class Issuer
    {
        public Issuer(TaxpayerIdentificationNumber nif, Name name)
        {
            Nif = nif;
            Name = name;
        }

        public TaxpayerIdentificationNumber Nif { get; }

        public Name Name { get; }
    }
}
