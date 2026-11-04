namespace Mews.Fiscalizations.Czechia.Model;

/// <summary>
/// Identifier of the taxpayer's registrating unit (branch) assigned in the MOJE daně portal (id_jednotky), 1 to 999 999 999.
/// </summary>
public sealed class RegistrationUnitIdentifier
{
    private RegistrationUnitIdentifier(int value)
    {
        Value = value;
    }

    public int Value { get; }

    public static Try<RegistrationUnitIdentifier, Error> Create(int value)
    {
        return IntValidations.InRange(value, min: 1, max: 999_999_999).Map(v => new RegistrationUnitIdentifier(v));
    }

    public static RegistrationUnitIdentifier CreateUnsafe(int value)
    {
        return Create(value).GetUnsafe();
    }

    public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);
}
