namespace Mews.Fiscalizations.Germany.Utils;

internal static class Extensions
{
    public static DateTime FromUnixTime(this long value)
    {
        return DateTimeOffset.FromUnixTimeSeconds(value).DateTime;
    }
}