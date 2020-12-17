using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FuncSharp;

namespace Mews.Fiscalization.Core.Model
{
    public interface ISequence<T> : IReadOnlyList<Indexed<T>>
    {
        INonEmptyEnumerable<Indexed<T>> Values { get; }

        int StartIndex { get; }
    }

    public class Sequence<T> : ISequence<T>
    {
        protected Sequence(INonEmptyEnumerable<Indexed<T>> values)
        {
            Values = values;
            Check.Condition(values.IsSequential(v => v.Index, startIndex: values.First().Index), "Item indexes are not sequential.");
        }

        public INonEmptyEnumerable<Indexed<T>> Values { get; }

        public int StartIndex => Values.First().Index;

        public int Count => Values.Count();

        public Indexed<T> this[int index] => Values.ElementAt(index - StartIndex);

        public IEnumerator<Indexed<T>> GetEnumerator()
        {
            return Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static ITry<ISequence<T>, string> Create(IEnumerable<Indexed<T>> indexedValues)
        {
            var orderedItems = indexedValues.OrderBy(item => item.Index).AsNonEmpty();
            var items = orderedItems.ToTry(_ => "Sequence cannot be empty.");

            var sequentialItems = items.Where(
                evaluator: i => i.IsSequential(item => item.Index, startIndex: i.First().Index),
                error: _ => "Item indexes are not sequential."
            );
            return sequentialItems.Map(i => new Sequence<T>(i));
        }

        public static IOption<ISequence<T>> FromPreordered(IEnumerable<T> values, int startIndex)
        {
            return values.AsNonEmpty().Map(v => FromPreordered(v, startIndex));
        }

        public static ISequence<T> FromPreordered(INonEmptyEnumerable<T> values, int startIndex)
        {
            return new Sequence<T>(values.Select((value, index) => new Indexed<T>(startIndex + index, value)));
        }
    }

    public static class SequentialEnumerable
    {
        public static IOption<ISequence<T>> FromPreordered<T>(IEnumerable<T> values, int startIndex)
        {
            return Sequence<T>.FromPreordered(values, startIndex);
        }

        public static ISequence<T> FromPreordered<T>(INonEmptyEnumerable<T> values, int startIndex)
        {
            return Sequence<T>.FromPreordered(values, startIndex);
        }

        public static ITry<ISequence<T>, string> Create<T>(IEnumerable<Indexed<T>> indexedItems)
        {
            return Sequence<T>.Create(indexedItems);
        }
    }
}
