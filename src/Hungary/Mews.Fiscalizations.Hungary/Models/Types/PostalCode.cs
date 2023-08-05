using FuncSharp;
using Mews.Fiscalizations.Core.Model;
using System.Text.RegularExpressions;

namespace Mews.Fiscalizations.Hungary.Models;

public sealed class PostalCode
{
    private PostalCode(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Try<PostalCode, Error> Create(string value)
    {
        return StringValidations.NonEmptyNorWhitespace(value).FlatMap(v =>
        {
            var validPostalCode = StringValidations.RegexMatch(v, new Regex("[A-Z0-9][A-Z0-9\\s\\-]{1,8}[A-Z0-9]"));
            return validPostalCode.Map(c => new PostalCode(c));
        });
    }
}