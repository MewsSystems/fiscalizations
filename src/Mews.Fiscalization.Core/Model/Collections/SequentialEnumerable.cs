using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mews.Fiscalization.Core.Extensions;
using Mews.Fiscalization.Greece.Model.Collections;

namespace Mews.Fiscalization.Core.Model
{
    public interface ISequentialEnumerable<out T> : IReadOnlyList<IIndexedItem<T>>
    {
        IEnumerable<T> Items { get; }

        IEnumerable<int> Indexes { get; }

        int StartIndex { get; }
    }

    public sealed class SequentialEnumerable<T> : ISequentialEnumerable<T>
    {
        private IReadOnlyList<IIndexedItem<T>> Values { get; }

        public IEnumerable<T> Items
        {
            get { return Values.Select(v => v.Item); }
        }

        public IEnumerable<int> Indexes
        {
            get { return Values.Select(v => v.Index); }
        }

        public int StartIndex
        {
            get { return Indexes.Min(); }
        }

        public SequentialEnumerable(IEnumerable<IndexedItem<T>> indexedItems)
        {
            Values = indexedItems.OrderBy(i => i.Index).AsList();

            if (!Values.NonEmpty() || !Values.IsSequential(startIndex: Indexes.Min()))
            {
                throw new ArgumentException("Item indexes are not sequential.", nameof(indexedItems));
            }
        }

        public IEnumerator<IIndexedItem<T>> GetEnumerator()
        {
            return Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count
        {
            get { return Values.Count; }
        }

        public IIndexedItem<T> this[int index]
        {
            get { return Values.ElementAt(index - StartIndex); }
        }
    }

    public static class SequentialEnumerable
    {
        public static ISequentialEnumerable<T> FromPreordered<T>(IEnumerable<T> source, int startIndex)
        {
            return new SequentialEnumerable<T>(source.Select((item, index) => new IndexedItem<T>(startIndex + index, item)));
        }

        public static ISequentialEnumerable<T> Create<T>(IEnumerable<IndexedItem<T>> source)
        {
            return new SequentialEnumerable<T>(source);
        }
    }
}
