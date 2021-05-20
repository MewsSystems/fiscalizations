namespace Mews.Eet.Dto.Identifiers
{
    public class BillNumber : StringIdentifier
    {
        public BillNumber(string value)
            : base(value, Patterns.BillNumber)
        {
        }
    }
}
