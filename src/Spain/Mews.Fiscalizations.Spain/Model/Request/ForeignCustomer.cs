using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Spain.Model.Request
{
    public class ForeignCustomer
    {
        public ForeignCustomer(Name name, ResidenceCountryIdentificatorType identificatiorType, String1To20 idNumber, Country country)
        {
            Name = name;
            IdentificatorType = identificatiorType;
            IdNumber = idNumber;
            Country = country;
        }

        public Name Name { get; }

        public ResidenceCountryIdentificatorType IdentificatorType { get; }

        public String1To20 IdNumber { get; }

        public Country Country { get; }
    }
}
