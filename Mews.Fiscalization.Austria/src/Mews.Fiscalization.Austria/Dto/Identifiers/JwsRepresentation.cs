using System.Text.RegularExpressions;

namespace Mews.Fiscalization.Austria.Dto.Identifiers
{
    public class JwsRepresentation : StringIdentifier
    {
        public static readonly Regex Pattern = new Regex(".+");

        public JwsRepresentation(string value)
            : base(value, Pattern)
        {
            Signature = new JwsSignature(value.Split('.')[2]);
        }

        public JwsSignature Signature { get; }
    }
}
