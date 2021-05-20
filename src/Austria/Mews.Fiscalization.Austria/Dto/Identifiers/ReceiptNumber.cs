using System.Text.RegularExpressions;

namespace Mews.Fiscalization.Austria.Dto.Identifiers
{
    public class ReceiptNumber : StringIdentifier
    {
        public static readonly Regex Pattern = new Regex(".+");

        public ReceiptNumber(string value)
            : base(value, Pattern)
        {
        }
    }
}
