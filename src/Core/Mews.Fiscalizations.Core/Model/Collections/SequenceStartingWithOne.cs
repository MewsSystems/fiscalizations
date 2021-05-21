using System.Collections.Generic;
using FuncSharp;

namespace Mews.Fiscalizations.Core.Model
{
    public interface ISequenceStartingWithOne<T> : ISequence<T>
    {
    }

    public sealed class SequenceStartingWithOne<T> : ISequenceStartingWithOne<T>
    {
        private SequenceStartingWithOne(ISequence<T> values)
        {
            Check.Condition(values.Values.Head.Index == 1, "Indexes must start with one.");
            Sequence = values;
        }

        public ISequence<T> Sequence { get; }

        public INonEmptyEnumerable<Indexed<T>> Values => Sequence.Values;

        public int StartIndex => Sequence.StartIndex;

        public static ITry<ISequenceStartingWithOne<T>, INonEmptyEnumerable<Error>> Create(IEnumerable<Indexed<T>> values)
        {
            var sequence = Sequence<T>.Create(values);
            var sequenceStartingWithOne = sequence.Where(
                evaluator: s => s.Values.Head.Index == 1,
                error: _ => new Error("Indexes must start with one.")
            );
            return sequenceStartingWithOne.Map(s => new SequenceStartingWithOne<T>(s));
        }

        public static IOption<ISequenceStartingWithOne<T>> FromPreordered(IEnumerable<T> values)
        {
            var sequence = Sequence<T>.FromPreordered(values, startIndex: 1);
            return sequence.Map(s => new SequenceStartingWithOne<T>(s));
        }

        public static ISequenceStartingWithOne<T> FromPreordered(INonEmptyEnumerable<T> values)
        {
            var sequence = Sequence<T>.FromPreordered(values, startIndex: 1);
            return new SequenceStartingWithOne<T>(sequence);
        }
    }

    public static class SequenceStartingWithOne
    {
        public static IOption<ISequenceStartingWithOne<T>> FromPreordered<T>(IEnumerable<T> values)
        {
            return SequenceStartingWithOne<T>.FromPreordered(values);
        }

        public static ISequenceStartingWithOne<T> FromPreordered<T>(INonEmptyEnumerable<T> values)
        {
            return SequenceStartingWithOne<T>.FromPreordered(values);
        }

        public static ITry<ISequenceStartingWithOne<T>, INonEmptyEnumerable<Error>> Create<T>(IEnumerable<Indexed<T>> values)
        {
            return SequenceStartingWithOne<T>.Create(values);
        }
    }
}