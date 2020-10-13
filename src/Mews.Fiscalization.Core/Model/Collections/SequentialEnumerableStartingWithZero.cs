using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalization.Core.Model
{
    public interface ISequentialEnumerableStartingWithZero<out T> : ISequentialEnumerable<T>
    {
    }

    public sealed class SequentialEnumerableStartingWithZero<T> : FixedStartSequentialEnumerable<T>, ISequentialEnumerableStartingWithZero<T>
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