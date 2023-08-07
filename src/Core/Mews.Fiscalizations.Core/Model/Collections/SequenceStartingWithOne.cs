using System.Collections.Generic;
using FuncSharp;

namespace Mews.Fiscalizations.Core.Model;

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

    public static Try<SequenceStartingWithOne<T>, Error> Create(IEnumerable<Indexed<T>> values)
    {
        var sequence = Sequence<T>.Create(values);
        var sequenceStartingWithOne = sequence.Where(
            predicate: s => s.Values.Head.Index == 1,
            otherwise: _ => new Error("Indexes must start with one.")
        );
        return sequenceStartingWithOne.Map(s => new SequenceStartingWithOne<T>(s));
    }

    public static IOption<SequenceStartingWithOne<T>> FromPreordered(IEnumerable<T> values)
    {
        var sequence = Sequence<T>.FromPreordered(values, startIndex: 1);
        return sequence.Map(s => new SequenceStartingWithOne<T>(s));
    }

    public static SequenceStartingWithOne<T> FromPreordered(INonEmptyEnumerable<T> values)
    {
        var sequence = Sequence<T>.FromPreordered(values, startIndex: 1);
        return new SequenceStartingWithOne<T>(sequence);
    }
}

public static class SequenceStartingWithOne
{
    public static IOption<SequenceStartingWithOne<T>> FromPreordered<T>(IEnumerable<T> values)
    {
        return SequenceStartingWithOne<T>.FromPreordered(values);
    }

    public static SequenceStartingWithOne<T> FromPreordered<T>(INonEmptyEnumerable<T> values)
    {
        return SequenceStartingWithOne<T>.FromPreordered(values);
    }

    public static Try<SequenceStartingWithOne<T>, Error> Create<T>(IEnumerable<Indexed<T>> values)
    {
        return SequenceStartingWithOne<T>.Create(values);
    }
}