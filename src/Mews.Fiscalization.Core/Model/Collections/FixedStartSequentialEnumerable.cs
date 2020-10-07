using System;
using System.Collections.Generic;
using Mews.Fiscalization.Greece.Model.Collections;

namespace Mews.Fiscalization.Core.Model.Collections
{
    public abstract class FixedStartSequentialEnumerable<T> : SequentialEnumerable<T>
    {
        protected FixedStartSequentialEnumerable(IEnumerable<IndexedItem<T>> indexedItems, int startIndex)
            : base(indexedItems)
        {
            if (StartIndex != startIndex)
            {
                throw new ArgumentException($"Items need to be indexed from {startIndex}.", nameof(indexedItems));
            }
        }
    }
}