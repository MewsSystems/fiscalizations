using System.Text.RegularExpressions;

namespace Mews.Fiscalizations.Czechia.Model;

/// <summary>
/// Identifier of the point of sale (id_pokl), 1 to 20 characters from [0-9a-zA-Z.,:;/#-_ ].
/// </summary>
public sealed class RegistryIdentifier
{
    private static readonly Regex Pattern = EetString.Pattern(maxLength: 20);

    private RegistryIdentifier(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Try<RegistryIdentifier, Error> Create(string value)
    {
        return StringValidations.RegexMatch(value, Pattern).Map(v => new RegistryIdentifier(v));
    }

    public static RegistryIdentifier CreateUnsafe(string value)
    {
        return Create(value).GetUnsafe();
    }

    public override string ToString() => Value;
}
