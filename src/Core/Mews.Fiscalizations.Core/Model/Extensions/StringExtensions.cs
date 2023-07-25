using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using FuncSharp;

namespace Mews.Fiscalizations.Core.Model;

public static class StringExtensions
{
    public static bool IsNotNullNorWhitespace(this string s)
    {
        return !IsNullOrWhitespace(s);
    }

    public static bool IsNullOrWhitespace(this string s)
    {
        return string.IsNullOrEmpty(s) || string.IsNullOrEmpty(s.Trim());
    }

    public static string StripDiacritics(this string s)
    {
        if (string.IsNullOrWhiteSpace(s))
        {
            return s;
        }

        var simpleChars = s.Normalize(NormalizationForm.FormD).Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark);
        return new string(simpleChars.ToArray());
    }

    public static string ToBasicLatin(this string value)
    {
        return string.IsNullOrWhiteSpace(value).Match(
            t => value,
            f => Regex.Replace(value.StripDiacritics(), @"[^\p{IsBasicLatin}]+", "")
        );
    }
}