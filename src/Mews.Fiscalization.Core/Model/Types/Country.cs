using System.Collections.Generic;

namespace Mews.Fiscalization.Core.Model
{
    public class Country : LimitedString
    {
        private static readonly StringLimitation Limitation = new StringLimitation(allowEmptyOrWhiteSpace: false, allowedValues: CountryInfo.AllCountryCodes);

        public Country(string alpha2Code)
            : this(alpha2Code, Limitation.ToEnumerable())
        {
        }

        protected Country(string alpha2Code, IEnumerable<StringLimitation> limitations)
            : base(alpha2Code, Limitation.Concat(limitations))
        {
        }

        public static bool IsValid(string alpha2Code)
        {
            return IsValid(alpha2Code, Limitation.ToEnumerable());
        }

        public new static bool IsValid(string alpha2Code, IEnumerable<StringLimitation> limitations)
        {
            return LimitedString.IsValid(alpha2Code, Limitation.Concat(limitations));
        }
    }
}
