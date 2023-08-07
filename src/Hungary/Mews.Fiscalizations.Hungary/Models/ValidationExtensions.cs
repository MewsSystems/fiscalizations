using FuncSharp;
using Mews.Fiscalizations.Core.Model;
using System.Text.RegularExpressions;

namespace Mews.Fiscalizations.Hungary.Models;

internal static class ValidationExtensions
{
    internal static Try<string, Error> ValidateString(string value, int minLength, int maxLength, string regex)
    {
        return StringValidations.LengthInRange(value, minLength, maxLength).FlatMap(v => StringValidations.RegexMatch(v, new Regex(regex)));
    }
}