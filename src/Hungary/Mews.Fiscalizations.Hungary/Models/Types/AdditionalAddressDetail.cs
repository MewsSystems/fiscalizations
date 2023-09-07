namespace Mews.Fiscalizations.Hungary.Models;

public sealed class AdditionalAddressDetail
{
    private AdditionalAddressDetail(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Try<AdditionalAddressDetail, Error> Create(string value)
    {
        return ValidationExtensions.ValidateString(value, minLength: 1, maxLength: 255, regex: ".*[^\\s].*").Map(v => new AdditionalAddressDetail(v));
    }
}