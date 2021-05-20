using System;
using System.Text.RegularExpressions;

namespace Mews.Fiscalization.Austria.Dto.Identifiers
{
    public sealed class JwsSignature : StringIdentifier
    {
        public static readonly Regex Pattern = new Regex(".+");

        public JwsSignature(string value)
            : base(value, Pattern)
        {
            Base64String = ToBase64String();
        }

        public string Base64String { get; }

        private string ToBase64String()
        {
            // The Value is Base64UrlSafe, the output should be plain Base64.
            var sanitizedString = Value.Replace('-', '+').Replace('_', '/');
            var paddingLength = sanitizedString.Length % 4;
            return paddingLength > 0 ? sanitizedString + new String('=', 4 - paddingLength) : sanitizedString;
        }
    }
}
