using FuncSharp;
using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Spain.Model
{
    public sealed class ForeignCompayTaxpayerNumber
    {
        private ForeignCompayTaxpayerNumber(Country country, string value)
        {
            Country = country;
            Value = value;
        }

        public Country Country { get; }

        public string Value { get; }

        public static ITry<ForeignCompayTaxpayerNumber, Error> Create(Country country, string value)
        {
            return StringValidations.LengthInRange(value, 1, 20).FlatMap(i =>
            {
                var validatedTaxId = TaxpayerIdentificationNumber.Create(country, i);
                return validatedTaxId.Map(n => new ForeignCompayTaxpayerNumber(n.Country, n.TaxpayerNumber));
            });
        }
    }
}
