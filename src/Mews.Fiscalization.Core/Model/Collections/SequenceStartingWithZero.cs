using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FuncSharp;

namespace Mews.Fiscalization.Core.Model
{
    public interface ISequenceStartingWithZero<T> : ISequence<T>
    {
    }

    public sealed class SequenceStartingWithZero<T> : ISequenceStartingWithZero<T>
    {
        private SequenceStartingWithZero(ISequence<T> values)
        {
            Check.Condition(values.Values.Head.Index == 0, "Indexes must start with zero.");
            Sequence = values;
        }

        public ISequence<T> Sequence { get; }

        public INonEmptyEnumerable<Indexed<T>> Values => Sequence.Values;

        public int StartIndex => Sequence.StartIndex;

        public static ITry<ISequenceStartingWithZero<T>, IEnumerable<Error>> Create(IEnumerable<Indexed<T>> values)
        {
            var sequence = Sequence<T>.Create(values);
            var sequenceStartingWithZero = sequence.Where(
                evaluator: s => s.Values.Head.Index == 0,
                error: _ => new Error("Indexes must start with zero.").ToEnumerable()
            );
            return sequenceStartingWithZero.Map(s => new SequenceStartingWithZero<T>(s)).MapError(e => e.Flatten());
        }

        public static IOption<ISequenceStartingWithZero<T>> FromPreordered(IEnumerable<T> values)
        {
            var sequence = Sequence<T>.FromPreordered(values, startIndex: 0);
            return sequence.Map(s => new SequenceStartingWithZero<T>(s));
        }

        public static ISequenceStartingWithZero<T> FromPreordered(INonEmptyEnumerable<T> values)
        {
            var sequence = Sequence<T>.FromPreordered(values, startIndex: 0);
            return new SequenceStartingWithZero<T>(sequence);
        }
    }

    public static class SequenceStartingWithZero
    {
        public static IOption<ISequenceStartingWithZero<T>> FromPreordered<T>(IEnumerable<T> values)
        {
            return SequenceStartingWithZero<T>.FromPreordered(values);
        }

        public static ISequenceStartingWithZero<T> FromPreordered<T>(INonEmptyEnumerable<T> values)
        {
            return SequenceStartingWithZero<T>.FromPreordered(values);
        }

        public static ITry<ISequenceStartingWithZero<T>, IEnumerable<Error>> Create<T>(IEnumerable<Indexed<T>> values)
        {
            return SequenceStartingWithZero<T>.Create(values);
        }
    }
}