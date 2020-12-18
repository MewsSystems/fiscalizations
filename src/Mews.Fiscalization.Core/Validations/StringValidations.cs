using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FuncSharp;

namespace Mews.Fiscalization.Core.Model
{
    public static class StringValidations
    {
        public static ITry<string, string> LengthInRange(string value, int min, int max)
        {
            var nonNullValue = NonNull(value);
            return nonNullValue.FlatMap(v =>
            {
                var validLength = IntValidations.InRange(v.Length, min: min, max: max);
                return validLength.Map(val => v).MapError(e => $"Length must be between {min} and {max}");
            });
        }

        public static ITry<string, string> RegexMatch(string value, string pattern)
        {
            return value.ToTry(v => v.IsNotNull() && new Regex(pattern).IsMatch(value), _ => $"Value '{value}' doesn't match the regex pattern '{pattern}'.");
        }

        public static ITry<string, string> In(string value, IEnumerable<string> allowedValues)
        {
            return value.ToTry(v => allowedValues.Contains(v), _ => $"Value '{value}' is not in allowed values.");
        }

        public static ITry<string, string> NonNull(string value)
        {
            return value.ToTry(v => v.IsNotNull(), _ => "Value cannot be null.");
        }

        public static ITry<string, string> NonEmpty(string value)
        {
            return value.ToTry(v => !string.IsNullOrEmpty(v), _ => "Value cannot be null or empty.");
        }

        public static ITry<string, string> NonEmptyOrWhitespace(string value)
        {
            return value.ToTry(v => !string.IsNullOrWhiteSpace(v), _ => "Value cannot be null, empty or whitespace.");
        }
    }
}