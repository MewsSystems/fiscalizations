using System;
using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalization.Core.Model
{
    public static class CollectionExtensions
    {
        public static List<T> AsList<T>(this IEnumerable<T> source)
        {
            return source as List<T> ?? source.ToList();
        }

        public static IEnumerable<T> ExceptNulls<T>(this IEnumerable<T> source)
        {
            return source.Where(x => x.IsNotNull());
        }

        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || !source.Any();
        }

        public static bool NonEmpty<T>(this IEnumerable<T> source)
        {
            return source != null && source.FirstOrDefault() != null;
        }

        public static bool IsSequential<T>(this IEnumerable<T> source, Func<T, int> indexGetter, int startIndex)
        {
            return source.Select(indexGetter).IsSequential(startIndex);
        }

        public static bool IsSequential(this IEnumerable<int> source, int startIndex)
        {
            return source.Select((value, index) => (value, index)).All(x => x.value == startIndex + x.index);
        }
    }
}
