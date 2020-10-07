﻿namespace Mews.Fiscalization.Greece.Model.Collections
{
    public interface IIndexedItem<out T>
    {
        int Index { get; }
        T Value { get; }
    }

    public sealed class IndexedItem<T> : IIndexedItem<T>
    {
        public IndexedItem(int index, T value)
        {
            Index = index;
            Value = value;
        }

        public int Index { get; }

        public T Value { get; }
    }
}
