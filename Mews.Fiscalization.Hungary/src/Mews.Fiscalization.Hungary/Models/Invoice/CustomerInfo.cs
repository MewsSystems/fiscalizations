using Mews.Fiscalization.Core.Model;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class CustomerInfo : Info
    {
        public CustomerInfo(TaxpayerIdentificationNumber taxpayerId, Name name, SimpleAddress address)
            : base(taxpayerId, name, address)
        {
        }
    }
}
