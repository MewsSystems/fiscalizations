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

        public static INonEmptyEnumerable<T> ToEnumerable<T>(this T value)
        {
            return NonEmptyEnumerable.Create(value);
        }

        public static INonEmptyEnumerable<T> Concat<T>(this T value, IEnumerable<T> others)
        {
            return NonEmptyEnumerable.Create(value, others);
        }
    }
}
