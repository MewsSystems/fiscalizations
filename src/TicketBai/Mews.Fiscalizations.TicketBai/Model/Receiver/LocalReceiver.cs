using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.TicketBai.Model
{
    public sealed class LocalReceiver : ReceiverInfo
    {
        public LocalReceiver(TaxpayerIdentificationNumber taxpayerIdentificationNumber, Name name, PostalCode postalCode, String1To250 address)
            : base(name, postalCode, address)
        {
            TaxpayerIdentificationNumber = Check.IsNotNull(taxpayerIdentificationNumber, nameof(taxpayerIdentificationNumber));
        }

        public TaxpayerIdentificationNumber TaxpayerIdentificationNumber { get; }
    }
}