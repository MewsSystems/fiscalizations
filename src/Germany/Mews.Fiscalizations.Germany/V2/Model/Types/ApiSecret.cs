using FuncSharp;
using Mews.Fiscalizations.Core.Model;
using System.Text.RegularExpressions;

namespace Mews.Fiscalizations.Germany.V2.Model
{
    public sealed class ApiSecret
    {
        private ApiSecret(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static ITry<ApiSecret, Error> Create(string value)
        {
            return StringValidations.RegexMatch(value, new Regex("^[0-9A-Za-z]{43}$")).Map(s => new ApiSecret(s));
        }
    }
}
