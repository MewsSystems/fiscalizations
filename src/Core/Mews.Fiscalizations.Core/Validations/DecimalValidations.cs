namespace Mews.Fiscalizations.Core.Model;

public static class DecimalValidations
{
    public static Try<decimal, Error> InRange(decimal value, decimal min, decimal max, bool minIsAllowed = true, bool maxIsAllowed = true)
    {
        return IComparableValidations.InRange(value, min, max, minIsAllowed, maxIsAllowed);
    }

    public static Try<decimal, Error> SmallerThan(decimal value, decimal limit)
    {
        return IComparableValidations.SmallerThan(value, limit);
    }

    public static Try<decimal, Error> SmallerThanOrEqual(decimal value, decimal limit)
    {
        return IComparableValidations.SmallerThanOrEqual(value, limit);
    }

    public static Try<decimal, Error> HigherThan(decimal value, decimal limit)
    {
        return IComparableValidations.HigherThan(value, limit);
    }

    public static Try<decimal, Error> HigherThanOrEqual(decimal value, decimal limit)
    {
        return IComparableValidations.HigherThanOrEqual(value, limit);
    }

    public static Try<decimal, Error> MaxDecimalPlaces(decimal value, int limit)
    {
        return value.ToTry(
            condition: v =>
            {
                var divisor = (decimal)Math.Pow(0.1, limit);
                return value % divisor == 0;
            },
            error: _ => new Error($"Highest possible precision is {limit} decimal places.")
        );
    }
}