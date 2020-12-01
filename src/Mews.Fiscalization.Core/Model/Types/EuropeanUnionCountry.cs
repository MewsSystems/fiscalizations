using System.Collections.Generic;

namespace Mews.Fiscalization.Core.Model
{
    public sealed class EuropeanUnionCountry : Country
    {
        private static readonly StringLimitation Limitation = new StringLimitation(allowedValues: CountryInfo.EuropeanUnionCountryCodes);

        public EuropeanUnionCountry(string alpha2Code) 
            : base(alpha2Code, Limitation.ToEnumerable())
        {
            Check.Condition(IsValid(alpha2Code), $"Invalid European Union country code.");
        }

        public new static bool IsValid(string alpha2Code)
        {
            return Country.IsValid(alpha2Code, Limitation);
        }

        public new static bool IsValid(string alpha2Code, IEnumerable<StringLimitation> limitations)
        {
            return Country.IsValid(alpha2Code, Limitation.Concat(limitations));
        }
    }
}
