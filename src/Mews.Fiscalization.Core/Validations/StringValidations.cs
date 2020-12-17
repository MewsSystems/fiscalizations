using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FuncSharp;

namespace Mews.Fiscalization.Core.Model
{
    public static class StringValidations
    {
        public static ITry<string, string> LengthInRange(string value, decimal min, decimal max, bool minIsAllowed = true, bool maxIsAllowed = true)
        {
            // TODO - Will call the int validation when it's merged.
            throw new NotImplementedException();
        }

        public static ITry<string, string> RegexMatch(string value, string pattern)
        {
            return value.ToTry(v => v.IsNotNull() && new Regex(pattern).IsMatch(value), _ => $"Value '{value}' doesn't match the regex pattern '{pattern}'.");
        }

        public static ITry<string, string> In(string value, IEnumerable<string> allowedValues)
        {
            return value.ToTry(v => allowedValues.Contains(v), _ => $"Value '{value}' is not in allowed values.");
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