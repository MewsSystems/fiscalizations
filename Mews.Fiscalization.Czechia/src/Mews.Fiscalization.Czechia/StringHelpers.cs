using System;
using System.Linq;
using System.Text.RegularExpressions;
using Mews.Eet.Converters;

namespace Mews.Eet
{
    /// <summary>
    /// This class is introduced as a replacement of StringExtensions in order to avoid polluting global space.
    /// </summary>
    public static class StringHelpers
    {
        public static string FormatForEet(DateTimeWithTimeZone dateTime)
        {
            return DateTimeConverter.ToEetDateTime(dateTime).ToString("yyyy-MM-dd'T'HH:mm:sszzz");
        }

        public static string FormatForEet(decimal amount)
        {
            return String.Format(System.Globalization.NumberFormatInfo.InvariantInfo, "{0:F2}", amount);
        }

        public static string TransformToBase16(byte[] hash)
        {
            return String.Concat(hash.Select(b => b.ToString("X2")));
        }

        public static string FormatOctets(string str)
        {
            //// Separate group of 8 characters by a dash. (?!$) is negative lookeahead (last group of 8 is not matched).
            return Regex.Replace(str, ".{8}(?!$)", "$0-");
        }
    }
}
