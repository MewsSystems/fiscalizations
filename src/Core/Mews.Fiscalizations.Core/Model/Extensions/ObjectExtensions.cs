using System;
using System.Collections.Generic;
using FuncSharp;

namespace Mews.Fiscalizations.Core.Model
{
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

        public static ITry<T, E> ToTry<T, E>(this T value, Func<T, bool> condition, Func<Unit, E> error)
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

        public static bool InRange<T>(this T value, T? from = null, T? to = null, bool closed = true)
            where T : struct, IComparable
        {
            if (from.MatchVal(f => value.Preceeds(f) || !closed && value.Equals(f)) ||
                to.MatchVal(t => value.Succeeds(t) || !closed && value.Equals(t)))
            {
                return false;
            }
            return true;
        }

        public static bool MatchVal<A>(this A? a, Func<A, bool> func)
            where A : struct
        {
            return MatchVal(a, func, _ => false);
        }

        public static B MatchVal<A, B>(this A? a, Func<A, B> func, Func<Unit, B> otherwise)
            where A : struct
        {
            return a.IsNotNull().Match(
                t => func(a.Value),
                f => otherwise(Unit.Value)
            );
        }
    }
}
