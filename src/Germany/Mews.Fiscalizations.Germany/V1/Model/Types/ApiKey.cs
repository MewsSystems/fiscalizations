using FuncSharp;
using Mews.Fiscalizations.Core.Model;
using System.Text.RegularExpressions;

namespace Mews.Fiscalizations.Germany.V1.Model
{
    public sealed class ApiKey
    {
        private ApiKey(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static ITry<ApiKey, INonEmptyEnumerable<Error>> Create(string value)
        {
            return StringValidations.LengthInRange(value, 1, 512).FlatMap(v =>
            {
                var validApiKey = StringValidations.RegexMatch(v, new Regex(".*[^\\s].*"));
                return validApiKey.Map(k => new ApiKey(k));
            });
        }
    }
}
