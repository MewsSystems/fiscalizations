using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Mews.Fiscalizations.Italy.Dto
{
    internal static class DtoUtils
    {
        public static decimal NormalizeDecimal(decimal value, int precision = 2)
        {
            return Math.Round(value + 0.00m, precision);
        }

        public static decimal? NormalizeDecimal(decimal? value, int precision = 2)
        {
            return value == null ? value : NormalizeDecimal(value.Value, precision);
        }

        public static string NormalizeZip(this string zip)
        {
            return Regex.Replace(zip, "[^0-9]", "");
        }

        public static string NonEmptyValueOrNull(this string str)
        {
            return String.IsNullOrEmpty(str) ? null : str;
        }

        public static string StripDiacritics(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return s.NonEmptyValueOrNull();
            }

            var simpleChars = s.Normalize(NormalizationForm.FormD).Where(c => !CharUnicodeInfo.GetUnicodeCategory(c).Equals(UnicodeCategory.NonSpacingMark));
            return new string(simpleChars.ToArray());
        }
    }
}
