using System.Collections.Generic;
using FuncSharp;

namespace Mews.Fiscalizations.Core.Model;

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

    public static Try<SequenceStartingWithZero<T>, Error> Create(IEnumerable<Indexed<T>> values)
    {
        var sequence = Sequence<T>.Create(values);
        var sequenceStartingWithZero = sequence.Where(
            predicate: s => s.Values.Head.Index == 0,
            otherwise: _ => new Error("Indexes must start with zero.")
        );
        return sequenceStartingWithZero.Map(s => new SequenceStartingWithZero<T>(s));
    }

    public static IOption<SequenceStartingWithZero<T>> FromPreordered(IEnumerable<T> values)
    {
        var sequence = Sequence<T>.FromPreordered(values, startIndex: 0);
        return sequence.Map(s => new SequenceStartingWithZero<T>(s));
    }

    public static SequenceStartingWithZero<T> FromPreordered(INonEmptyEnumerable<T> values)
    {
        var sequence = Sequence<T>.FromPreordered(values, startIndex: 0);
        return new SequenceStartingWithZero<T>(sequence);
    }
}

public static class SequenceStartingWithZero
{
    public static IOption<SequenceStartingWithZero<T>> FromPreordered<T>(IEnumerable<T> values)
    {
        return SequenceStartingWithZero<T>.FromPreordered(values);
    }

    public static SequenceStartingWithZero<T> FromPreordered<T>(INonEmptyEnumerable<T> values)
    {
        return SequenceStartingWithZero<T>.FromPreordered(values);
    }

    public static Try<SequenceStartingWithZero<T>, Error> Create<T>(IEnumerable<Indexed<T>> values)
    {
        return SequenceStartingWithZero<T>.Create(values);
    }
}