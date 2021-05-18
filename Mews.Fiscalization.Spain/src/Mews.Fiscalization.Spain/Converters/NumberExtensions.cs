using System.Globalization;

namespace Mews.Fiscalization.Spain.Converters
{
    public static class NumberExtensions
    {
        internal static string Serialize(this decimal value)
        {
            return value.ToString("F2", CultureInfo.InvariantCulture);
        }
    }
}
