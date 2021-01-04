using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FuncSharp;

namespace Mews.Fiscalization.Core.Model
{
    public interface INonEmptyEnumerable<out T> : IEnumerable<T>
    {
        T Head { get; }

        IReadOnlyList<T> Tail { get; }

        INonEmptyEnumerable<TResult> Select<TResult>(Func<T, TResult> func);

        INonEmptyEnumerable<TResult> Select<TResult>(Func<T, int, TResult> func);
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

        public static IOption<INonEmptyEnumerable<T>> Create<T>(IEnumerable<T> values)
        {
            return Create(values.ToList());
        }

        public static IOption<INonEmptyEnumerable<T>> Create<T>(List<T> values)
        {
            return values.FirstOption().Map(h => new NonEmptyEnumerable<T>(h, values.Skip(1)));
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

        public INonEmptyEnumerable<TResult> Select<TResult>(Func<T, TResult> func)
        {
            return new NonEmptyEnumerable<TResult>(func(Head), Tail.Select(func));
        }

        public INonEmptyEnumerable<TResult> Select<TResult>(Func<T, int, TResult> func)
        {
            return new NonEmptyEnumerable<TResult>(func(Head, 0), Tail.Select((v, i) => func(v, i + 1)));
        }

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