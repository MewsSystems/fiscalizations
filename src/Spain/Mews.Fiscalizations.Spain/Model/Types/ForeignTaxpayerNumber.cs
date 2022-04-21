using FuncSharp;
using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Spain.Model
{
    public sealed class ForeignTaxpayerNumber
    {
        private ForeignTaxpayerNumber(Country country, string value)
        {
            Country = Check.IsNotNull(country, nameof(country));
            Value = Check.IsNotNull(value, nameof(value));
        }

        public Country Country { get; }

        public string Value { get; }

        public static ITry<ForeignTaxpayerNumber, Error> Create(Country country, string value)
        {
            return StringValidations.LengthInRange(value, 1, 20).FlatMap(i =>
            {
                var validatedForeignCountry = country.ToOption().Where(c => !c.Equals(Countries.Spain)).ToTry(_ => new Error("Country can't be Spain for foreign tax payers."));
                var validatedTaxId = validatedForeignCountry.FlatMap(c => TaxpayerIdentificationNumber.Create(c, i));
                return validatedTaxId.Map(n => new ForeignTaxpayerNumber(n.Country, n.TaxpayerNumber));
            });
        }
    }
}
