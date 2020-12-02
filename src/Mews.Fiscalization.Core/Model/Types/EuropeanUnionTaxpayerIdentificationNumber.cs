using System.Collections.Generic;
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
            var matchesRegex = Regex.IsMatch(taxpayerNumber, CountryInfo.EuropeanUnionTaxpayerNumberPatterns[country.Value]);

            return matchesRegex && IsValid(taxpayerNumber);
        }

        public new static bool IsValid(string taxpayerNumber)
        {
            return IsValid(taxpayerNumber, Limitation.ToEnumerable());
        }

        public new static bool IsValid(string taxpayerNumber, IEnumerable<StringLimitation> limitations)
        {
            return LimitedString.IsValid(taxpayerNumber, Limitation.Concat(limitations));
        }
    }
}
