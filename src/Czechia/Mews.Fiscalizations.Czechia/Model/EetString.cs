using System.Text.RegularExpressions;

namespace Mews.Fiscalizations.Czechia.Model;

internal static class EetString
{
    private const string AllowedCharacters = @"[0-9a-zA-Z\.,:;/#\-_ ]";

    public static Regex Pattern(int maxLength)
    {
        return new Regex($"^{AllowedCharacters}{{1,{maxLength}}}$", RegexOptions.Compiled);
    }
}
