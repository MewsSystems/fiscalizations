using System.Globalization;

namespace Mews.Fiscalizations.Czechia.Model;

/// <summary>
/// Amount in CZK with at most two decimal places and an absolute value below 100 000 000 (celk_trzba, urceno_cerp_zuct, cerp_zuct).
/// </summary>
public sealed class CurrencyValue
{
    private const decimal Limit = 100_000_000m;

    private CurrencyValue(decimal value)
    {
        Value = value;
    }

    public decimal Value { get; }

    public static Try<CurrencyValue, Error> Create(decimal value)
    {
        return DecimalValidations.InRange(value, min: -Limit, max: Limit, minIsAllowed: false, maxIsAllowed: false)
            .FlatMap(v => DecimalValidations.MaxDecimalPlaces(v, 2))
            .Map(v => new CurrencyValue(v));
    }

    public static CurrencyValue CreateUnsafe(decimal value)
    {
        return Create(value).GetUnsafe();
    }

    /// <summary>
    /// Canonical representation required by the interface: exactly two decimals, no leading zeros and no "-0.00".
    /// </summary>
    public override string ToString()
    {
        return Value == 0m ? "0.00" : Value.ToString("0.00", CultureInfo.InvariantCulture);
    }
}
