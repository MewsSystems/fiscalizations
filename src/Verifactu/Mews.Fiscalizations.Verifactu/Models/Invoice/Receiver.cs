using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Verifactu.Models;

public sealed class Receiver : Coproduct2<LocalReceiver, ForeignReceiver>
{
    public Receiver(LocalReceiver localReceiver)
        : base(localReceiver)
    {
    }

    public Receiver(ForeignReceiver foreignReceiver)
        : base(foreignReceiver)
    {
    }

    public string Address => Match(
        localReceiver => localReceiver.Address,
        foreignReceiver => foreignReceiver.Address
    );

    public string PostalCode => Match(
        localReceiver => localReceiver.PostalCode,
        foreignReceiver => foreignReceiver.PostalCode
    );

    public static Receiver CreateLocal(string name, string taxIdentifier, string address, string postalCode)
    {
        return new Receiver(new LocalReceiver(name, taxIdentifier, address, postalCode));
    }

    public static Receiver CreateForeign(string name, ForeignerDocumentType type, string number, Country documentCountry, string address, string postalCode)
    {
        return new Receiver(new ForeignReceiver(name, type, number, documentCountry, address, postalCode));
    }
}