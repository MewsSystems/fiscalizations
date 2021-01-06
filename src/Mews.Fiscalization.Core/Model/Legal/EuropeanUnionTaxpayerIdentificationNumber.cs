using FuncSharp;

namespace Mews.Fiscalization.Core.Model
{
    public sealed class EuropeanUnionTaxpayerIdentificationNumber
    {
        private EuropeanUnionTaxpayerIdentificationNumber(EuropeanUnionCountry country, string taxpayerNumber)
        {
            Country = country;
            TaxpayerNumber = taxpayerNumber;
        }

        public EuropeanUnionCountry Country { get; }

        public string TaxpayerNumber { get; }

        public static ITry<EuropeanUnionTaxpayerIdentificationNumber, INonEmptyEnumerable<Error>> Create(EuropeanUnionCountry country, string taxpayerNumber)
        {
            return ObjectValidations.NotNull(country).FlatMap(c =>
            {
                var validNumber = StringValidations.RegexMatch(taxpayerNumber, country.TaxpayerNumberPattern);
                return validNumber.Map(n => new EuropeanUnionTaxpayerIdentificationNumber(c, n));
            });
        }
    }
}
