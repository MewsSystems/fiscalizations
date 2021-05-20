using Mews.Fiscalization.Core.Model;

namespace Mews.Fiscalization.Spain.Model.Request
{
    public sealed class ForeignCompany
    {
        public ForeignCompany(Name name, ResidenceCountryIdentificatorType identificatiorType, String1To20 id, Country country)
        {
            Name = name;
            IdentificatorType = identificatiorType;
            Id = id;
            Country = country;
        }

        public Name Name { get; }

        public ResidenceCountryIdentificatorType IdentificatorType { get; }

        public String1To20 Id { get; }

        public Country Country { get; }
    }
}
