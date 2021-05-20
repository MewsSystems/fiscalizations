using System.Text.RegularExpressions;
using FuncSharp;

namespace Mews.Fiscalization.Core.Model
{
    public class EuropeanUnionCountry
    {
        internal EuropeanUnionCountry(string alpha2Code, Regex taxpayerNumberPattern)
        {
            Alpha2Code = alpha2Code;
            TaxpayerNumberPattern = taxpayerNumberPattern;
        }

        public string Alpha2Code { get; }

        public Regex TaxpayerNumberPattern { get; }

        public static IOption<EuropeanUnionCountry> GetByCode(string alpha2Code)
        {
            return Countries.GetEuropeanUnionByCode(alpha2Code);
        }
    }
}

