using FuncSharp;

namespace Mews.Fiscalization.Core.Model
{
    public class Country : Coproduct2<EuropeanUnionCountry, NonEuropeanUnionCountry>
    {
        internal Country(EuropeanUnionCountry firstValue)
            : base(firstValue)
        {
        }

        internal Country(NonEuropeanUnionCountry secondValue)
            : base(secondValue)
        {
        }

        public string Alpha2Code
        {
            get
            {
                return Match(
                    europeanUnionCountry => europeanUnionCountry.Alpha2Code,
                    nonEuropeanUnionCountry => nonEuropeanUnionCountry.Alpha2Code
                );
            }
        }

        public static IOption<Country> GetByCode(string alpha2Code)
        {
            return Countries.GetByCode(alpha2Code);
        }
    }
}
