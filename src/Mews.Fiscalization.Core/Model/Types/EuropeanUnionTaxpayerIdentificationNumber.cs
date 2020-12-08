using System.Text.RegularExpressions;

namespace Mews.Fiscalization.Core.Model
{
    public sealed class EuropeanUnionTaxpayerIdentificationNumber : TaxpayerIdentificationNumber
    {
        private static readonly StringLimitation Limitation = new StringLimitation(allowEmptyOrWhiteSpace: false);

        public EuropeanUnionTaxpayerIdentificationNumber(EuropeanUnionCountry country, string taxpayerNumber)
            : base(country, taxpayerNumber)
        {
            Check.IsNotNull(country, nameof(country));
            Check.Condition(IsValid(country, taxpayerNumber), "Invalid European union taxpayer identification number.");
        }

        public static bool IsValid(EuropeanUnionCountry country, string taxpayerNumber)
        {
            if (!IsValid(taxpayerNumber, Limitation.ToEnumerable()) || country.IsNull())
            {
                return false;
            }
            return Regex.IsMatch(taxpayerNumber, CountryInfo.EuropeanUnionTaxpayerNumberPatterns[country.Value]);
        }
    }
}
