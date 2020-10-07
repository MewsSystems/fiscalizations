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
        public static ISequentialEnumerable<T> FromPreordered<T>(IEnumerable<T> source)
        {
            return new SequentialEnumerable<T>(source.Select((item, index) => new IndexedItem<T>(index + 1, item)));
        }

        public static ISequentialEnumerable<T> Create<T>(IEnumerable<IndexedItem<T>> source)
        {
            return new SequentialEnumerableStartingWithOne<T>(source);
        }
    }
}