using System.Collections.Generic;
using System.Linq;
using Mews.Fiscalization.Greece.Model.Collections;

namespace Mews.Fiscalization.Core.Model.Collections
{
    public sealed class SequentialEnumerableStartingWithZero<T> : FixedStartSequentialEnumerable<T>
    {
        public SequentialEnumerableStartingWithZero(IEnumerable<IndexedItem<T>> indexedItems)
            : base(indexedItems, startIndex: 0)
        {
        }
    }

    public static class SequentialEnumerableStartingWithZero
    {
        public static SequentialEnumerableStartingWithZero<T> FromPreordered<T>(params T[] source)
        {
            return FromPreordered(source.AsEnumerable());
        }

        public static SequentialEnumerableStartingWithZero<T> FromPreordered<T>(IEnumerable<T> source)
        {
            return Create(source.Select((item, index) => new IndexedItem<T>(index, item)));
        }

        public static SequentialEnumerableStartingWithZero<T> Create<T>(IEnumerable<IndexedItem<T>> source)
        {
            return new SequentialEnumerableStartingWithZero<T>(source);
        }
    }
}