using System.Text.RegularExpressions;
using Mews.Fiscalization.Austria.Dto.Identifiers;

namespace Mews.Fiscalization.Austria.ATrust
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
