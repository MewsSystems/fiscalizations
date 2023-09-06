namespace Mews.Fiscalizations.Hungary.Models;

public sealed class City
{
    private City(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Try<City, Error> Create(string value)
    {
        return ValidationExtensions.ValidateString(value, minLength: 1, maxLength: 255, regex: ".*[^\\s].*").Map(v => new City(v));
    }
}