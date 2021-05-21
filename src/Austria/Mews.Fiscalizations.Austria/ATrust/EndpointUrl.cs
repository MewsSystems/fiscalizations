using System.Text.RegularExpressions;
using Mews.Fiscalizations.Austria.Dto.Identifiers;

namespace Mews.Fiscalizations.Austria.ATrust
{
    public sealed class EndpointUrl : StringIdentifier
    {
        public static readonly Regex Pattern = new Regex("https?://.+[^/]");

        public EndpointUrl(string url)
            : base(url, Pattern)
        {
        }
    }
}
