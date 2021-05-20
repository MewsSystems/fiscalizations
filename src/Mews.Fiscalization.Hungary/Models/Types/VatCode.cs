using FuncSharp;
using Mews.Fiscalization.Core.Model;
using System.Text.RegularExpressions;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class VatCode
    {
        private VatCode(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static ITry<VatCode, INonEmptyEnumerable<Error>> Create(string value)
        {
            return StringValidations.NonEmptyNorWhitespace(value).FlatMap(v =>
            {
                var validVatCode = StringValidations.RegexMatch(v, new Regex("[1-5]{1}"));
                return validVatCode.Map(c => new VatCode(c));
            });
        }
    }
}
