using System;
using System.Collections.Generic;
using System.Linq;
using FuncSharp;

namespace Mews.Fiscalization.Core.Model
{
    public interface ISequence<T>
    {
        INonEmptyEnumerable<Indexed<T>> Values { get; }

        int StartIndex { get; }
    }

    public class Sequence<T> : ISequence<T>
    {
        private Sequence(INonEmptyEnumerable<Indexed<T>> values)
        {
            Values = values;
        }

        public INonEmptyEnumerable<Indexed<T>> Values { get; }

        public int StartIndex => Values.Head.Index;

        public static ITry<ISequence<T>, INonEmptyEnumerable<Error>> Create(IEnumerable<Indexed<T>> indexedValues)
        {
            var orderedItems = indexedValues.OrderBy(item => item.Index).AsNonEmpty();
            var items = orderedItems.ToTry(_ => Error.Create("Sequence cannot be empty."));

            var sequentialItems = items.Where(
                evaluator: i => i.IsSequential(item => item.Index, startIndex: i.First().Index),
                error: _ => new Error("Item indexes are not sequential.")
            );
            return sequentialItems.Map(i => new Sequence<T>(i));
        }

        public static IOption<ISequence<T>> FromPreordered(IEnumerable<T> values, int startIndex)
        {
            return values.AsNonEmpty().Map(v => FromPreordered(v, startIndex));
        }

        public static ISequence<T> FromPreordered(INonEmptyEnumerable<T> values, int startIndex)
        {
            var result = Create(values.Select((value, index) => new Indexed<T>(startIndex + index, value)));
            return result.Get(e => throw new Exception($"{nameof(FromPreordered)} resulted in an invalid sequence."));
        }
    }

    public static class Sequence
    {
        public static IOption<ISequence<T>> FromPreordered<T>(IEnumerable<T> values, int startIndex)
        {
            return Sequence<T>.FromPreordered(values, startIndex);
        }

        public static ISequence<T> FromPreordered<T>(INonEmptyEnumerable<T> values, int startIndex)
        {
            return Sequence<T>.FromPreordered(values, startIndex);
        }

        public static ITry<ISequence<T>, INonEmptyEnumerable<Error>> Create<T>(IEnumerable<Indexed<T>> indexedItems)
        {
            return Sequence<T>.Create(indexedItems);
        }
    }
}
