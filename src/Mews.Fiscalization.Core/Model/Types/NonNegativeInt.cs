using System.Collections.Generic;

namespace Mews.Fiscalization.Core.Model
{
    public class NonNegativeInt : LimitedInt
    {
        private static readonly RangeLimitation<int> Limitation = new RangeLimitation<int>(min: 0);

        public NonNegativeInt(int value)
            : base(value, Limitation)
        {
        }

        public new static bool IsValid(int value, RangeLimitation<int> limitation = null)
        {
            return IsValid(value, Limitation.Concat(limitation.ToEnumerable()).ExceptNulls());
        }

        public new static bool IsValid(int value, IEnumerable<RangeLimitation<int>> limitation)
        {
            return LimitedInt.IsValid(value, Limitation.Concat(limitation));
        }
    }
}