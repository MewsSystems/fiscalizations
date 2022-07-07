using FuncSharp;
using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Basque.Model
{
    public sealed class ForeignReceiver : ReceiverInfo
    {
        public ForeignReceiver(IdType idType, String1To20 id, Name name, PostalCode postalCode, String1To250 address, Country country = null)
            : base(name, postalCode, address)
        {
            IdType = Check.IsNotNull(idType, nameof(idType));
            Id = Check.IsNotNull(id, nameof(id));
            Country = country.ToOption();
        }

        public IdType IdType { get; }

        public String1To20 Id { get; }

        public IOption<Country> Country { get; }
    }
}