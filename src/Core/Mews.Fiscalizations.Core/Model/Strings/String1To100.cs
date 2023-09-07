namespace Mews.Fiscalizations.Core.Model;

public sealed class String1To100
{
    private String1To100(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Try<String1To100, Error> Create(string value)
    {
        return StringValidations.LengthInRange(value, 1, 100).Map(v => new String1To100(v));
    }

    public static String1To100 CreateUnsafe(string value)
    {
        return Create(value).GetUnsafe();
    }
}