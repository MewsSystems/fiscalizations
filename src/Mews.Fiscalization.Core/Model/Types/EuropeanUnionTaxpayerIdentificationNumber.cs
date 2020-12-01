using System.Text.RegularExpressions;

namespace Mews.Fiscalization.Core.Model
{
    public sealed class EuropeanUnionTaxpayerIdentificationNumber : TaxpayerIdentificationNumber
    {
        public EuropeanUnionTaxpayerIdentificationNumber(EuropeanUnionCountry country, string taxpayerNumber)
            : base(country, taxpayerNumber)
        {
        }

        public static bool IsValid(EuropeanUnionCountry country, string taxpayerNumber)
        {
            return Regex.IsMatch(taxpayerNumber, CountryInfo.EuropeanUnionTaxpayerNumberPatterns[country.Value]);
        }
    }
}
