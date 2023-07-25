using System;
using System.Text.RegularExpressions;

namespace Mews.Fiscalizations.Italy.Dto;

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

    public static string NonEmptyValueOrNull(this string s)
    {
        return String.IsNullOrEmpty(s) ? null : s;
    }
}