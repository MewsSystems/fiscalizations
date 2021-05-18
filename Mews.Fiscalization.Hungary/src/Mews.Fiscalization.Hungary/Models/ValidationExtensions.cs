using FuncSharp;
using Mews.Fiscalization.Core.Model;
using System.Text.RegularExpressions;

namespace Mews.Fiscalization.Hungary.Models
{
    internal static class ValidationExtensions
    {
        internal static ITry<string, INonEmptyEnumerable<Error>> ValidateString(string value, int minLength, int maxLength, string regex)
        {
            return StringValidations.LengthInRange(value, minLength, maxLength).FlatMap(v => StringValidations.RegexMatch(v, new Regex(regex)));
        }
    }
}
