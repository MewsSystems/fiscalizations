using System.Collections.Generic;

namespace Mews.Fiscalization.Core.Model
{
    public class PositiveInt : LimitedInt
    {
        private static readonly RangeLimitation<int> Limitation = new RangeLimitation<int>(min: 1);

        public PositiveInt(int value)
            : base(value, Limitation)
        {
        }

        public static bool IsValid(int value)
        {
            return IsValid(value, Limitation);
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