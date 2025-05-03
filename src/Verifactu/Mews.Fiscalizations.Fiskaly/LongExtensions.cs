namespace Mews.Fiscalizations.Fiskaly;

internal static class LongExtensions
{
    public static DateTime FromUnixTime(this long value)
    {
        return DateTimeOffset.FromUnixTimeSeconds(value).DateTime;
    }
}