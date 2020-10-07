﻿using Mews.Fiscalization.Greece.Model.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalization.Core.Extensions
{
    public static class CollectionExtensions
    {
        public static List<TSource> AsList<TSource>(this IEnumerable<TSource> source)
        {
            return source as List<TSource> ?? source.ToList();
        }

        public static bool NonEmpty<T>(this IEnumerable<T> source)
        {
            return source != null && source.FirstOrDefault() != null;
        }

        internal static bool IsSequential<T>(this IReadOnlyList<IIndexedItem<T>> source, int startIndex)
        {
            var expectedIndices = new HashSet<int>(Enumerable.Range(start: startIndex, count: source.Count));
            var actualIndices = source.Select(i => i.Index);
            return expectedIndices.SetEquals(actualIndices);
        }
    }
}
