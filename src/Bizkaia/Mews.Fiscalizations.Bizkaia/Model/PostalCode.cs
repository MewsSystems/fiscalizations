using System.Text.RegularExpressions;

namespace Mews.Fiscalizations.Bizkaia.Model;

public sealed class PostalCode
{
    private PostalCode(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Try<PostalCode, Error> Create(string value)
    {
        return StringValidations.RegexMatch(value, new Regex("^[a-zA-Z0-9]{1,20}$")).Map(v => new PostalCode(v));
    }

    public static PostalCode CreateUnsafe(string value)
    {
        return Create(value).GetUnsafe();
    }
}