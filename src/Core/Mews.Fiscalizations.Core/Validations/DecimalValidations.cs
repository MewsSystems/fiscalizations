using System;
using FuncSharp;

namespace Mews.Fiscalizations.Core.Model
{
    public static class DecimalValidations
    {
        public static ITry<decimal, INonEmptyEnumerable<Error>> InRange(decimal value, decimal min, decimal max, bool minIsAllowed = true, bool maxIsAllowed = true)
        {
            return IComparableValidations.InRange(value, min, max, minIsAllowed, maxIsAllowed);
        }

        public static ITry<decimal, INonEmptyEnumerable<Error>> SmallerThan(decimal value, decimal limit)
        {
            return IComparableValidations.SmallerThan(value, limit);
        }

        public static ITry<decimal, INonEmptyEnumerable<Error>> SmallerThanOrEqual(decimal value, decimal limit)
        {
            return IComparableValidations.SmallerThanOrEqual(value, limit);
        }

        public static ITry<decimal, INonEmptyEnumerable<Error>> HigherThan(decimal value, decimal limit)
        {
            return IComparableValidations.HigherThan(value, limit);
        }

        public static ITry<decimal, INonEmptyEnumerable<Error>> HigherThanOrEqual(decimal value, decimal limit)
        {
            return IComparableValidations.HigherThanOrEqual(value, limit);
        }

        public static ITry<decimal, INonEmptyEnumerable<Error>> MaxDecimalPlaces(decimal value, int limit)
        {
            return value.ToTry(
                condition: v =>
                {
                    var divisor = (decimal)Math.Pow(0.1, limit);
                    return value % divisor == 0;
                },
                error: _ => Error.Create($"Highest possible precision is {limit} decimal places.")
            );
        }
    }
}