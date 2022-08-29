using System.Text.RegularExpressions;
using FuncSharp;

namespace Mews.Fiscalizations.Core.Model
{
    public class EuropeanUnionCountry
    {
        internal EuropeanUnionCountry(string alpha2Code, Regex regexWithCountryCodePrefix, Regex regexWithoutCountryCodePrefix)
        {
            Alpha2Code = alpha2Code;
            RegexWithCountryCodePrefix = regexWithCountryCodePrefix;
            RegexWithoutCountryCodePrefix = regexWithoutCountryCodePrefix;
        }

        public string Alpha2Code { get; }

        public Regex RegexWithCountryCodePrefix { get; }

        public Regex RegexWithoutCountryCodePrefix { get; }

        public static IOption<EuropeanUnionCountry> GetByCode(string alpha2Code)
        {
            return Countries.GetEuropeanUnionByCode(alpha2Code);
        }
    }
}

