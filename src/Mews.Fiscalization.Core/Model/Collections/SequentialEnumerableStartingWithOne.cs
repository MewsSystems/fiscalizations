using System.Collections.Generic;
using System.Linq;
using Mews.Fiscalization.Greece.Model.Collections;

namespace Mews.Fiscalization.Core.Model.Collections
{
    public sealed class SequentialEnumerableStartingWithOne<T> : FixedStartSequentialEnumerable<T>
    {
        public SequentialEnumerableStartingWithOne(IEnumerable<IndexedItem<T>> indexedItems)
            : base(indexedItems, startIndex: 1)
        {
        }
    }

    public static class SequentialEnumerableStartingWithOne
    {
        public static SequentialEnumerableStartingWithOne<T> FromPreordered<T>(params T[] source)
        {
            return FromPreordered(source.AsEnumerable());
        }

        public static SequentialEnumerableStartingWithOne<T> FromPreordered<T>(IEnumerable<T> source)
        {
            return Create(source.Select((item, index) => new IndexedItem<T>(index + 1, item)));
        }

        public static SequentialEnumerableStartingWithOne<T> Create<T>(IEnumerable<IndexedItem<T>> source)
        {
            return new SequentialEnumerableStartingWithOne<T>(source);
        }
    }
}