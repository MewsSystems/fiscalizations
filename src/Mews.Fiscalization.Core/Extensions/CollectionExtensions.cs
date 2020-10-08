﻿using System;
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
