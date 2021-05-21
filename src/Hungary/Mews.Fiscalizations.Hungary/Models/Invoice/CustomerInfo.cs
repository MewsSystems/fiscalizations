using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Hungary.Models
{
    public sealed class CustomerInfo : Info
    {
        public CustomerInfo(TaxpayerIdentificationNumber taxpayerId, Name name, SimpleAddress address)
            : base(taxpayerId, name, address)
        {
        }
    }
}
