using Mews.Fiscalization.Core.Model;

namespace Mews.Fiscalization.Hungary.Models
{
    public class Info
    {
        public Info(TaxpayerIdentificationNumber taxpayerId, Name name, SimpleAddress address)
        {
            TaxpayerId = Check.IsNotNull(taxpayerId, nameof(taxpayerId));
            Name = Check.IsNotNull(name, nameof(name));
            Address = Check.IsNotNull(address, nameof(address));
        }

        public TaxpayerIdentificationNumber TaxpayerId { get; }

        public Name Name { get; }

        public SimpleAddress Address { get; }
    }
}
