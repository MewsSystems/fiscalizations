using FuncSharp;

namespace Mews.Fiscalizations.Core.Model
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

        public static ITry<EuropeanUnionTaxpayerIdentificationNumber, Error> Create(
            EuropeanUnionCountry country,
            string taxpayerNumber,
            bool isCountryCodePrefixAllowed = true)
        {
            return ObjectValidations.NotNull(country).FlatMap(c =>
            {
                var pattern = isCountryCodePrefixAllowed.Match(
                    t => country.TaxpayerNumberPattern,
                    f => country.TaxpayerNumberPatternWithoutCountryCodePrefix
                );
                return StringValidations.RegexMatch(taxpayerNumber.Trim(), pattern).Map(n => new EuropeanUnionTaxpayerIdentificationNumber(c, n));
            });
        }
    }
}
