using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FuncSharp;

namespace Mews.Fiscalizations.Core.Model;

public static class StringValidations
{
    public static Try<string, Error> LengthInRange(string value, int min, int max)
    {
        var nonNullValue = ObjectValidations.NotNull(value);
        return nonNullValue.FlatMap(v =>
        {
            var validLength = IntValidations.InRange(v.Length, min: min, max: max);
            return validLength.Map(val => v).MapError(e => new Error($"Length must be between {min} and {max}"));
        });
    }

    public static Try<string, Error> RegexMatch(string value, Regex pattern)
    {
        return value.ToTry(v => v is not null && pattern.IsMatch(v), _ => new Error($"Value '{value}' doesn't match the regex pattern '{pattern}'."));
    }

    public static Try<string, Error> In(string value, IEnumerable<string> allowedValues)
    {
        return value.ToTry(v => allowedValues.Contains(v), _ => new Error($"Value '{value}' is not in allowed values."));
    }

    public static Try<string, Error> NonEmpty(string value)
    {
        return value.ToTry(v => !string.IsNullOrEmpty(v), _ => new Error("Value cannot be null or empty."));
    }

    public static Try<string, Error> NonEmptyNorWhitespace(string value)
    {
        return value.ToTry(v => !string.IsNullOrWhiteSpace(v), _ => new Error("Value cannot be null, empty or whitespace."));
    }
}