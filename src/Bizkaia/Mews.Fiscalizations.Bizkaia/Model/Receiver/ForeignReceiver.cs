namespace Mews.Fiscalizations.Bizkaia.Model;

public sealed class ForeignReceiver : ReceiverInfo
{
    public ForeignReceiver(IdType idType, String1To20 id, Name name, PostalCode postalCode, String1To250 address, Country country)
        : base(name, postalCode, address)
    {
        IdType = Check.IsNotNull(idType, nameof(idType));
        Id = Check.IsNotNull(id, nameof(id));
        Country = Check.IsNotNull(country, nameof(country));
    }

    public IdType IdType { get; }

    public String1To20 Id { get; }

    public Country Country { get; }
}