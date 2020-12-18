using System;
using FuncSharp;

namespace Mews.Fiscalization.Core.Model
{
    public static class DecimalValidations
    {
        public static ITry<decimal, string> InRange(decimal value, decimal min, decimal max, bool minIsAllowed = true, bool maxIsAllowed = true)
        {
            return IComparableValidations.InRange(value, min, max, minIsAllowed, maxIsAllowed);
        }

        public static ITry<decimal, string> SmallerThan(decimal value, decimal limit)
        {
            return IComparableValidations.SmallerThan(value, limit);
        }

        public static ITry<decimal, string> SmallerThanOrEqual(decimal value, decimal limit)
        {
            return IComparableValidations.SmallerThanOrEqual(value, limit);
        }

        public static ITry<decimal, string> HigherThan(decimal value, decimal limit)
        {
            return IComparableValidations.HigherThan(value, limit);
        }

        public static ITry<decimal, string> HigherThanOrEqual(decimal value, decimal limit)
        {
            return IComparableValidations.HigherThanOrEqual(value, limit);
        }

        public static ITry<decimal, string> MaxDecimalPlaces(decimal value, int limit)
        {
            return value.ToTry(
                condition: v =>
                {
                    var divisor = (decimal)Math.Pow(0.1, limit);
                    return value % divisor == 0;
                },
                error: _ => $"Highest possible precision is {limit} decimal places."
            );
        }
    }
}