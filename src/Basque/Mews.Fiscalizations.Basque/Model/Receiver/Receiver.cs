using FuncSharp;
using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Basque.Model;

public sealed class Receiver : Coproduct2<LocalReceiver, ForeignReceiver>
{
    public Receiver(LocalReceiver localReceiver)
        : base(localReceiver)
    {
        Check.IsNotNull(localReceiver, nameof(localReceiver));
    }

    public Receiver(ForeignReceiver foreignReceiver)
        : base(foreignReceiver)
    {
        Check.IsNotNull(foreignReceiver, nameof(foreignReceiver));
    }

    public static ITry<Receiver, Error> Local(string nif, Name name, PostalCode postalCode, String1To250 address)
    {
        return LocalReceiver.Create(nif, name, postalCode, address).Map(r => new Receiver(r));
    }

    public static Receiver Foreign(IdType idType, String1To20 id, Name name, PostalCode postalCode, String1To250 address, Country country)
    {
        return new Receiver(new ForeignReceiver(idType, id, name, postalCode, address, country));
    }
}