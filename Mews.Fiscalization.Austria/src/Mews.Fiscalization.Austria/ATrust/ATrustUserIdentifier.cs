using System.Text.RegularExpressions;
using Mews.Fiscalization.Austria.Dto.Identifiers;

namespace Mews.Fiscalization.Austria.ATrust
{
    public sealed class ATrustUserIdentifier : StringIdentifier
    {
        public static readonly Regex Pattern = new Regex("u.+");

        public ATrustUserIdentifier(string value)
            : base(value, Pattern)
        {
        }

        public static bool IsValid(string value)
        {
            return IsValid(value, Pattern);
        }
    }
}
