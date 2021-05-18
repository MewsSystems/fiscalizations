namespace Mews.Eet.Dto.Identifiers
{
    public class PremisesIdentifier : IntIdentifier
    {
        public PremisesIdentifier(int value)
            : base(value, 1, 999999)
        {
        }
    }
}
