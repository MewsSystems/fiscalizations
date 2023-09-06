namespace Mews.Fiscalizations.Hungary.Models;

public sealed class AmountValue
{
    public AmountValue(decimal value)
    {
        Check.Digits(value, maxdigitCount: 18);
        Check.Precision(value, maxPrecision: 2);
        Value = value;
    }

    public decimal Value { get; }
}