using FuncSharp;
using System.Collections.Generic;

namespace Mews.Fiscalization.Core.Model
{
    public static class IntValidations
    {
        public static ITry<int, IEnumerable<Error>> InRange(int value, int min, int max, bool minIsAllowed = true, bool maxIsAllowed = true)
        {
            return IComparableValidations.InRange(value, min, max, minIsAllowed, maxIsAllowed);
        }

        public static ITry<int, IEnumerable<Error>> SmallerThan(int value, int limit)
        {
            return IComparableValidations.SmallerThan(value, limit);
        }

        public static ITry<int, IEnumerable<Error>> SmallerThanOrEqual(int value, int limit)
        {
            return IComparableValidations.SmallerThanOrEqual(value, limit);
        }

        public static ITry<int, IEnumerable<Error>> HigherThan(int value, int limit)
        {
            return IComparableValidations.HigherThan(value, limit);
        }

        public static ITry<int, IEnumerable<Error>> HigherThanOrEqual(int value, int limit)
        {
            return IComparableValidations.HigherThanOrEqual(value, limit);
        }
    }
}