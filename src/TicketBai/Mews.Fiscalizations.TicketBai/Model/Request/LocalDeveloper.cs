using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.TicketBai.Model
{
    public sealed class LocalDeveloper
    {
        public LocalDeveloper(TaxpayerIdentificationNumber nif)
        {
            Nif = nif;
        }

        public TaxpayerIdentificationNumber Nif { get; }
    }
}
