using System;
using FuncSharp;

namespace Mews.Fiscalization.Core.Model
{
    public static class IComparableValidations
    {
        public static ITry<T, INonEmptyEnumerable<Error>> InRange<T>(T value, T min, T max, bool minIsAllowed = true, bool maxIsAllowed = true)
            where T : struct, IComparable
        {
            var minCheckedValue = minIsAllowed.Match(
                t => HigherThanOrEqual(value, min),
                f => HigherThan(value, min)
            );
            return minCheckedValue.FlatMap(_ => maxIsAllowed.Match(
                t => SmallerThanOrEqual(value, max),
                f => SmallerThan(value, max)
            ));
        }

        public static ITry<T, INonEmptyEnumerable<Error>> SmallerThan<T>(T value, T limit)
            where T : struct, IComparable
        {
            return value.ToTry(v => v.Preceeds(limit), _ => Error.Create($"Value must be smaller than {limit}"));
        }

        public static ITry<T, INonEmptyEnumerable<Error>> SmallerThanOrEqual<T>(T value, T limit)
            where T : struct, IComparable
        {
            return value.ToTry(v => v.PreceedsOrEquals(limit), _ => Error.Create($"Value must be smaller than or equal to {limit}"));
        }

        public static ITry<T, INonEmptyEnumerable<Error>> HigherThan<T>(T value, T limit)
            where T : struct, IComparable
        {
            return value.ToTry(v => v.Succeeds(limit), _ => Error.Create($"Value must be higher than {limit}"));
        }

        public static ITry<T, INonEmptyEnumerable<Error>> HigherThanOrEqual<T>(T value, T limit)
            where T : struct, IComparable
        {
            return value.ToTry(v => v.SucceedsOrEquals(limit), _ => Error.Create($"Value must be higher than or equal to {limit}"));
        }
    }
}