using FuncSharp;
using Mews.Fiscalization.Core.Model;
using System.Text.RegularExpressions;

namespace Mews.Fiscalization.Germany.Model
{
    public sealed class ApiSecret
    {
        private ApiSecret(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static ITry<ApiSecret, INonEmptyEnumerable<Error>> Create(string value)
        {
            return StringValidations.RegexMatch(value, new Regex("^[0-9A-Za-z]{43}$")).Map(s => new ApiSecret(s));
        }
    }
}
