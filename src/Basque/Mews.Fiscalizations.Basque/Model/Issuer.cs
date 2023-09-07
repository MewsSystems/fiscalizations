namespace Mews.Fiscalizations.Basque.Model;

public sealed class Issuer
{
    public Issuer(TaxpayerIdentificationNumber nif, Name name)
    {
        Nif = Check.IsNotNull(nif, nameof(nif));
        Name = Check.IsNotNull(name, nameof(name));
    }

    public TaxpayerIdentificationNumber Nif { get; }

    public Name Name { get; }

    public static Try<Issuer, Error> Create(Name name, string nif)
    {
        return TaxpayerIdentificationNumber.Create(Countries.Spain, nif, isCountryCodePrefixAllowed: false).Map(n => new Issuer(n, name));
    }
}