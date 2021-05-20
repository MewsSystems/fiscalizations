using FuncSharp;
using Mews.Fiscalization.Core.Model;
using System.Text.RegularExpressions;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class PostalCode
    {
        private PostalCode(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static ITry<PostalCode, INonEmptyEnumerable<Error>> Create(string value)
        {
            return StringValidations.NonEmptyNorWhitespace(value).FlatMap(v =>
            {
                var validPostalCode = StringValidations.RegexMatch(v, new Regex("[A-Z0-9][A-Z0-9\\s\\-]{1,8}[A-Z0-9]"));
                return validPostalCode.Map(c => new PostalCode(c));
            });
        }
    }
}
