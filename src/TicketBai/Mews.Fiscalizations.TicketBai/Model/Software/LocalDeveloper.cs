using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.TicketBai.Model
{
    public sealed class LocalDeveloper
    {
        public LocalDeveloper(TaxpayerIdentificationNumber nif)
        {
            Nif = Check.IsNotNull(nif, nameof(nif));
        }

        public TaxpayerIdentificationNumber Nif { get; }
    }
}