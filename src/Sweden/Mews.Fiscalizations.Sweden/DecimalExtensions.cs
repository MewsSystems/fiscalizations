namespace Mews.Fiscalizations.Sweden;

public static class DecimalExtensions
{
    public static int ConvertToSmallestUnit(this decimal value)
    {
        return (int)(value * 100);
    }
}