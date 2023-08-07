using System;
using System.Collections.Generic;
using FuncSharp;

namespace Mews.Fiscalizations.Core.Model;

public static class ObjectExtensions
{
    public static bool IsNotNull<T>(this T value)
    {
        return !value.IsNull();
    }

    public static bool IsNull<T>(this T value)
    {
        return value == null;
    }

    public static bool Preceeds<T>(this T x, T y)
        where T : struct, IComparable
    {
        return x.CompareTo(y) < 0;
    }

    public static bool PreceedsOrEquals<T>(this T x, T y)
        where T : struct, IComparable
    {
        return x.CompareTo(y) <= 0;
    }

    public static bool Succeeds<T>(this T x, T y)
        where T : struct, IComparable
    {
        return x.CompareTo(y) > 0;
    }

    public static bool SucceedsOrEquals<T>(this T x, T y)
        where T : struct, IComparable
    {
        return x.CompareTo(y) >= 0;
    }

    public static Try<T, E> ToTry<T, E>(this T value, Func<T, bool> condition, Func<Unit, E> error)
    {
        return condition(value).Match(
            t => Try.Success<T, E>(value),
            f => Try.Error<T, E>(error(Unit.Value))
        );
    }

    public static bool Implies(this bool a, Func<bool> b)
    {
        return !a || b();
    }

    public static INonEmptyEnumerable<T> ToEnumerable<T>(this T value)
    {
        return NonEmptyEnumerable.Create(value);
    }

    public static INonEmptyEnumerable<T> Concat<T>(this T value, IEnumerable<T> others)
    {
        return NonEmptyEnumerable.Create(value, others);
    }

    public static bool HasFewerDigitsThan(this decimal value, int maxDigitCount)
    {
        return value < (decimal)Math.Pow(10, maxDigitCount);
    }

    public static bool PrecisionSmallerThanOrEqualTo(this decimal value, int maxPrecision)
    {
        var minAllowedFraction = (decimal)Math.Pow(10, -1 * maxPrecision);
        return value % minAllowedFraction == 0;
    }
}