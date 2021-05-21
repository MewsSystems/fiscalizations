using FuncSharp;

namespace Mews.Fiscalizations.Core.Model
{
    public static class IntValidations
    {
        public static ITry<int, INonEmptyEnumerable<Error>> InRange(int value, int min, int max, bool minIsAllowed = true, bool maxIsAllowed = true)
        {
            return IComparableValidations.InRange(value, min, max, minIsAllowed, maxIsAllowed);
        }

        public static ITry<int, INonEmptyEnumerable<Error>> SmallerThan(int value, int limit)
        {
            return IComparableValidations.SmallerThan(value, limit);
        }

        public static ITry<int, INonEmptyEnumerable<Error>> SmallerThanOrEqual(int value, int limit)
        {
            return IComparableValidations.SmallerThanOrEqual(value, limit);
        }

        public static ITry<int, INonEmptyEnumerable<Error>> HigherThan(int value, int limit)
        {
            return IComparableValidations.HigherThan(value, limit);
        }

        public static ITry<int, INonEmptyEnumerable<Error>> HigherThanOrEqual(int value, int limit)
        {
            return IComparableValidations.HigherThanOrEqual(value, limit);
        }
    }
}