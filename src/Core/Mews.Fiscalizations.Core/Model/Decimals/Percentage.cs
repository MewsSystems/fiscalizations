using FuncSharp;

namespace Mews.Fiscalizations.Core.Model;

public sealed class Percentage
{
    private Percentage(decimal value)
    {
        Value = value;
    }

    public decimal Value { get; }

    public static Try<Percentage, Error> Create(decimal value)
    {
        return DecimalValidations.InRange(value, 0, 100).FlatMap(v =>
        {
            var validatedValue = DecimalValidations.MaxDecimalPlaces(v, 2);
            return validatedValue.Map(p => new Percentage(p));
        });
    }
}