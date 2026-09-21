namespace Mews.Fiscalizations.Hungary.Models;

/// <summary>
/// NAV unitOfMeasure, plus the literal unitOfMeasureOwn that goes with it when the canonical list has no
/// value for the unit. NAV requires unitOfMeasureOwn whenever unitOfMeasure is OWN
/// (INCORRECT_LINE_DATA_UOM_INCOMPLETE), so the two are modelled together and cannot drift apart.
/// </summary>
public sealed class UnitOfMeasure
{
    private UnitOfMeasure(UnitOfMeasureKind kind, string ownValue)
    {
        Kind = kind;
        OwnValue = ownValue.ToOption();
    }

    public UnitOfMeasureKind Kind { get; }

    public Option<string> OwnValue { get; }

    public static UnitOfMeasure Piece()
    {
        return new UnitOfMeasure(UnitOfMeasureKind.Piece, null);
    }

    public static UnitOfMeasure Day()
    {
        return new UnitOfMeasure(UnitOfMeasureKind.Day, null);
    }

    /// <summary>
    /// A unit NAV has no canonical value for - a hotel night, for instance, which is neither PIECE nor DAY.
    /// </summary>
    public static Try<UnitOfMeasure, Error> Own(string ownValue)
    {
        return StringValidations.LengthInRange(ownValue, 1, 50).Map(v => new UnitOfMeasure(UnitOfMeasureKind.Own, v));
    }
}

/// <summary>
/// Named Kind rather than Type so it does not read as the generated <c>Dto.UnitOfMeasureType</c> it is
/// mapped onto, which is in scope in the same file as the mapping.
/// </summary>
public enum UnitOfMeasureKind
{
    Piece,
    Day,
    Own
}
