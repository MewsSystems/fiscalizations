using FuncSharp;

namespace Mews.Fiscalization.Core.Model
{
    public static class IntValidations
    {
        public static ITry<int, Error> InRange(int value, int min, int max, bool minIsAllowed = true, bool maxIsAllowed = true)
        {
            return IComparableValidations.InRange(value, min, max, minIsAllowed, maxIsAllowed);
        }

        public static ITry<int, Error> SmallerThan(int value, int limit)
        {
            return IComparableValidations.SmallerThan(value, limit);
        }

        public static ITry<int, Error> SmallerThanOrEqual(int value, int limit)
        {
            return IComparableValidations.SmallerThanOrEqual(value, limit);
        }

        public static ITry<int, Error> HigherThan(int value, int limit)
        {
            return IComparableValidations.HigherThan(value, limit);
        }

        public static ITry<int, Error> HigherThanOrEqual(int value, int limit)
        {
            return IComparableValidations.HigherThanOrEqual(value, limit);
        }
    }
}