using System;
using System.Collections.Generic;

namespace Mews.Fiscalization.Core.Model
{
    public static class ObjectExtensions
    {
        public static bool IsNotNull<T>(this T value)
        {
            return !value.IsNull();
        }

        public static bool IsNull<T>(this T value)
        {
            return value == null;
        }

        public static bool Implies(this bool a, Func<bool> b)
        {
            return !a || b();
        }

        public static INonEmptyEnumerable<T> ToEnumerable<T>(this T value)
        {
            return NonEmptyEnumerable.Create(value);
        }

        public static INonEmptyEnumerable<T> Concat<T>(this T value, IEnumerable<T> others)
        {
            return NonEmptyEnumerable.Create(value, others);
        }

        public static bool HasFewerDigitsThan(this decimal value, int maxDigitCount)
        {
            return value < (decimal)Math.Pow(10, maxDigitCount);
        }

        public static bool PrecisionSmallerThanOrEqualTo(this decimal value, int maxPrecision)
        {
            var minAllowedFraction = (decimal)Math.Pow(10, -1 * maxPrecision);
            return value % minAllowedFraction == 0;
        }
    }
}
