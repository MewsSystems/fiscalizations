namespace Mews.Fiscalizations.Spain.Model.Request;

public class ForeignCustomer
{
    public ForeignCustomer(Name name, ResidenceCountryIdentificatorType identificatiorType, String1To20 idNumber, Country country)
    {
        Name = Check.IsNotNull(name, nameof(name));
        IdentificatorType = Check.IsNotNull(identificatiorType, nameof(identificatiorType));
        IdNumber = Check.IsNotNull(idNumber, nameof(idNumber));
        Country = Check.IsNotNull(country, nameof(country));
    }

    public Name Name { get; }

    public ResidenceCountryIdentificatorType IdentificatorType { get; }

    public String1To20 IdNumber { get; }

    public Country Country { get; }
}