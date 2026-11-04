using System.Text.RegularExpressions;

namespace Mews.Fiscalizations.Czechia.Model;

/// <summary>
/// Sequence number of the sales transaction, usually the receipt number (porad_cis), 1 to 25 characters from [0-9a-zA-Z.,:;/#-_ ].
/// </summary>
public sealed class BillNumber
{
    private static readonly Regex Pattern = EetString.Pattern(maxLength: 25);

    private BillNumber(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Try<BillNumber, Error> Create(string value)
    {
        return StringValidations.RegexMatch(value, Pattern).Map(v => new BillNumber(v));
    }

    public static BillNumber CreateUnsafe(string value)
    {
        return Create(value).GetUnsafe();
    }

    public override string ToString() => Value;
}
