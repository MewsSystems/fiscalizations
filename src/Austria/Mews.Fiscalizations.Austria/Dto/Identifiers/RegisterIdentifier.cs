using System.Text.RegularExpressions;

namespace Mews.Fiscalizations.Austria.Dto.Identifiers;

public class RegisterIdentifier : StringIdentifier
{
    public static readonly Regex Pattern = new Regex(".+");

    public RegisterIdentifier(string value)
        : base(value, Pattern)
    {
    }
}