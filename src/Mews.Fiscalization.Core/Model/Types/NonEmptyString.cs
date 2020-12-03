using System.Collections.Generic;

namespace Mews.Fiscalization.Core.Model
{
    public class NonEmptyString : LimitedString
    {
        private static readonly StringLimitation Limitation = new StringLimitation(allowEmptyOrWhiteSpace: false);

        public NonEmptyString(string value)
            : base(value, Limitation.ToEnumerable())
        {
        }

        protected NonEmptyString(string value, IEnumerable<StringLimitation> limitations = null)
            : base(value, Limitation.Concat(limitations))
        {
        }

        public static bool IsValid(string value)
        {
            return IsValid(value, Limitation.ToEnumerable());
        }

        protected new static bool IsValid(string value, IEnumerable<StringLimitation> limitation)
        {
            return LimitedString.IsValid(value, Limitation.Concat(limitation));
        }
    }
}