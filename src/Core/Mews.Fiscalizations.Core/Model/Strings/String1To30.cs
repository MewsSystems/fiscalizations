namespace Mews.Fiscalizations.Core.Model;

public sealed class String1To30
{
    private String1To30(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Try<String1To30, Error> Create(string value)
    {
        return StringValidations.LengthInRange(value, 1, 30).Map(v => new String1To30(v));
    }

    public static String1To30 CreateUnsafe(string value)
    {
        return Create(value).GetUnsafe();
    }
}