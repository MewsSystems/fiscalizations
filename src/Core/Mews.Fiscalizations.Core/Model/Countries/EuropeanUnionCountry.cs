using System.Text.RegularExpressions;

namespace Mews.Fiscalizations.Core.Model;

public class EuropeanUnionCountry
{
    internal EuropeanUnionCountry(string alpha2Code, Regex taxpayerNumberPattern, Regex taxpayerNumberPatternWithoutCountryCodePrefix)
    {
        Alpha2Code = alpha2Code;
        TaxpayerNumberPattern = taxpayerNumberPattern;
        TaxpayerNumberPatternWithoutCountryCodePrefix = taxpayerNumberPatternWithoutCountryCodePrefix;
    }

    public string Alpha2Code { get; }

    public Regex TaxpayerNumberPattern { get; }

    public Regex TaxpayerNumberPatternWithoutCountryCodePrefix { get; }

    public static Option<EuropeanUnionCountry> GetByCode(string alpha2Code)
    {
        return Countries.GetEuropeanUnionByCode(alpha2Code);
    }
}