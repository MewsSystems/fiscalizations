using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mews.Fiscalization.Core.Model;

namespace Mews
{
    public interface INonEmptyEnumerable<out T> : IEnumerable<T>
    {
        T Head { get; }

        IReadOnlyList<T> Tail { get; }
    }

    public static class NonEmptyEnumerable
    {
        public static INonEmptyEnumerable<T> Create<T>(T head, params T[] tail)
        {
            return new NonEmptyEnumerable<T>(head, tail);
        }

        public static INonEmptyEnumerable<T> Create<T>(T head, IEnumerable<T> tail)
        {
            return new NonEmptyEnumerable<T>(head, tail);
        }

        public static INonEmptyEnumerable<T> Create<T>(IEnumerable<T> values)
        {
            var enumeratedValues = values.ToList();
            Check.NonEmpty(enumeratedValues, "Enumerable was empty.");
            return new NonEmptyEnumerable<T>(enumeratedValues.First(), enumeratedValues.Skip(1));
        }
    }

    internal class NonEmptyEnumerable<T> : INonEmptyEnumerable<T>, IReadOnlyList<T>
    {
        public NonEmptyEnumerable(T head, IEnumerable<T> tail)
        {
            Head = head;
            Tail = tail.ToList();
            var values = new List<T>{ head };
            values.AddRange(Tail);

            Values = values;
        }

        public T Head { get; }

        public IReadOnlyList<T> Tail { get; }

        public IReadOnlyList<T> Values { get; }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Values.GetEnumerator();
        }

        public int Count => Values.Count;

        public T this[int index] => Values[index];
    }
}