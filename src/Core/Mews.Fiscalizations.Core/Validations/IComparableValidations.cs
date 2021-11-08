using System;
using FuncSharp;

namespace Mews.Fiscalizations.Core.Model
{
    public static class IComparableValidations
    {
        public static ITry<T, Error> InRange<T>(T value, T min, T max, bool minIsAllowed = true, bool maxIsAllowed = true)
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

        public static ITry<T, Error> SmallerThan<T>(T value, T limit)
            where T : struct, IComparable
        {
            return value.ToTry(v => v.Preceeds(limit), _ => new Error($"Value must be smaller than {limit}"));
        }

        public static ITry<T, Error> SmallerThanOrEqual<T>(T value, T limit)
            where T : struct, IComparable
        {
            return value.ToTry(v => v.PreceedsOrEquals(limit), _ => new Error($"Value must be smaller than or equal to {limit}"));
        }

        public static ITry<T, Error> HigherThan<T>(T value, T limit)
            where T : struct, IComparable
        {
            return value.ToTry(v => v.Succeeds(limit), _ => new Error($"Value must be higher than {limit}"));
        }

        public static ITry<T, Error> HigherThanOrEqual<T>(T value, T limit)
            where T : struct, IComparable
        {
            return value.ToTry(v => v.SucceedsOrEquals(limit), _ => new Error($"Value must be higher than or equal to {limit}"));
        }
    }
}