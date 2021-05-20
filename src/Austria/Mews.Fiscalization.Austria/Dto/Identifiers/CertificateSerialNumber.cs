using System.Text.RegularExpressions;

namespace Mews.Fiscalization.Austria.Dto.Identifiers
{
    public sealed class CertificateSerialNumber : StringIdentifier
    {
        public static readonly Regex Pattern = new Regex(".+");

        public CertificateSerialNumber(string value)
            : base(value, Pattern)
        {
        }
    }
}
