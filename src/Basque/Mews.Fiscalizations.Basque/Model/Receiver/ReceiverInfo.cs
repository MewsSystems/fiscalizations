using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Basque.Model
{
    public class ReceiverInfo
    {
        public ReceiverInfo(Name name, PostalCode postalCode, String1To250 address)
        {
            Name = Check.IsNotNull(name, nameof(name));
            PostalCode = Check.IsNotNull(postalCode, nameof(postalCode));
            Address = Check.IsNotNull(address, nameof(address));
        }

        public Name Name { get; }

        public PostalCode PostalCode { get; }

        public String1To250 Address { get; }
    }
}