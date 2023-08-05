using System.Linq;
using FuncSharp;

namespace Mews.Fiscalizations.Core.Model;

public struct PositiveInt
{
    private PositiveInt(int value)
    {
        Value = value;
    }

    public int Value { get; }

    public static implicit operator int(PositiveInt i)
    {
        return i.Value;
    }

    public static implicit operator NonNegativeInt(PositiveInt i)
    {
        return NonNegativeInt.CreateUnsafe(i.Value);
    }

    public static PositiveInt operator +(PositiveInt a, NonNegativeInt b)
    {
        return a.Sum(b);
    }

    public static PositiveInt operator *(PositiveInt a, PositiveInt b)
    {
        return a.Multiply(b);
    }

    public static Try<PositiveInt, Error> Create(int value)
    {
        return IntValidations.HigherThan(value, 0).Map(v => new PositiveInt(v));
    }

    public static PositiveInt CreateUnsafe(int value)
    {
        return Create(value).GetUnsafe();
    }

    public PositiveInt Sum(params NonNegativeInt[] values)
    {
        return new PositiveInt(values.Aggregate(Value, (a, b) => a + b));
    }

    public PositiveInt Multiply(params PositiveInt[] values)
    {
        return new PositiveInt(values.Aggregate(Value, (a, b) => a * b));
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}