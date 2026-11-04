using System.Text.RegularExpressions;

namespace Mews.Fiscalizations.Czechia.Model;

/// <summary>
/// Registered identification number of a taxpayer (EIČ), e.g. CZ00000019 (eic_popl, eic_poverujiciho).
/// </summary>
public sealed class TaxIdentifier
{
    private static readonly Regex Pattern = new("^CZ[0-9]{8,10}$", RegexOptions.Compiled);

    private TaxIdentifier(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Try<TaxIdentifier, Error> Create(string value)
    {
        return StringValidations.RegexMatch(value, Pattern).Map(v => new TaxIdentifier(v));
    }

    public static TaxIdentifier CreateUnsafe(string value)
    {
        return Create(value).GetUnsafe();
    }

    public override string ToString() => Value;
}
