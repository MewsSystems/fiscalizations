namespace Mews.Fiscalization.Core.Model
{
    public class NonEuropeanUnionCountry
    {
        internal NonEuropeanUnionCountry(string alpha2Code)
        {
            Alpha2Code = alpha2Code;
        }

        public string Alpha2Code { get; }
    }
}
