using FuncSharp;

namespace Mews.Fiscalizations.Core.Model;

public sealed class String1To120
{
    private String1To120(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Try<String1To120, Error> Create(string value)
    {
        return StringValidations.LengthInRange(value, 1, 120).Map(v => new String1To120(v));
    }

    public static String1To120 CreateUnsafe(string value)
    {
        return Create(value).GetUnsafe();
    }
}