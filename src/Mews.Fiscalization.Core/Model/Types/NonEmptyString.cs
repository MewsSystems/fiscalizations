using System.Collections.Generic;

namespace Mews.Fiscalization.Core.Model
{
    public class NonEmptyString : LimitedString
    {
        private static readonly StringLimitation Limitation = new StringLimitation(allowEmptyOrWhiteSpace: false);

        public NonEmptyString(string value, StringLimitation limitation = null)
            : base(value, Limitation.Concat(limitation.ToEnumerable()).ExceptNulls())
        {
        }

        public new static bool IsValid(string value, StringLimitation limitation = null)
        {
            return IsValid(value, Limitation.Concat(limitation.ToEnumerable()).ExceptNulls());
        }

        public new static bool IsValid(string value, IEnumerable<StringLimitation> limitation)
        {
            return LimitedString.IsValid(value, Limitation.Concat(limitation));
        }
    }
}