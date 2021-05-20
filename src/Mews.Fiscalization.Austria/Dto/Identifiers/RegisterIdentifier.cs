using System.Text.RegularExpressions;

namespace Mews.Fiscalization.Austria.Dto.Identifiers
{
    public class RegisterIdentifier : StringIdentifier
    {
        public static readonly Regex Pattern = new Regex(".+");

        public RegisterIdentifier(string value)
            : base(value, Pattern)
        {
        }
    }
}
