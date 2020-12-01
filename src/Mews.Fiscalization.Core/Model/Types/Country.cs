using System.Collections.Generic;

namespace Mews.Fiscalization.Core.Model
{
    public class Country : NonEmptyString
    {
        private static readonly StringLimitation Limitation = new StringLimitation(allowedValues: CountryInfo.AllCountryCodes);

        public Country(string alpha2Code)
            : this(alpha2Code, Limitation.ToEnumerable())
        {
        }

        protected Country(string alpha2Code, IEnumerable<StringLimitation> limitations)
            : base(alpha2Code, Limitation.Concat(limitations))
        {
        }

        public new static bool IsValid(string alpha2Code)
        {
            return IsValid(alpha2Code, Limitation);
        }

        public new static bool IsValid(string alpha2Code, IEnumerable<StringLimitation> limitations)
        {
            return NonEmptyString.IsValid(alpha2Code, Limitation.Concat(limitations));
        }
    }
}
