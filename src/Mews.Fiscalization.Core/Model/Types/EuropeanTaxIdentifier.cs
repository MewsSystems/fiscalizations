using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Mews.Fiscalization.Core.Model
{
    public sealed class EuropeanTaxIdentifier
    {
        public EuropeanTaxIdentifier(string alpha2CountryCode, string taxIdentifier)
        {
            ValidateTaxIdentifier(alpha2CountryCode, taxIdentifier);
            Check.Condition(IsValid(alpha2CountryCode, taxIdentifier), "Invalid tax identifier.");
        }

        public static bool IsValid(string alpha2CountryCode, string taxIdentifier)
        {
            ValidateTaxIdentifier(alpha2CountryCode, taxIdentifier);
            return Regex.IsMatch(taxIdentifier, EuropeanTaxIdentifierPatterns[alpha2CountryCode]);
        }

        private static void ValidateTaxIdentifier(string alpha2CountryCode, string taxIdentifier)
        {
            Check.Condition(alpha2CountryCode.IsNotNullNorWhitespace(), "Country code cannot be null or empty.");
            Check.Condition(EuropeanTaxIdentifierPatterns.ContainsKey(alpha2CountryCode), "Invalid or not implemented country code.");
            Check.Condition(taxIdentifier.IsNotNullNorWhitespace(), "Tax identifier cannot be null or empty.");
        }
        
        private static readonly IReadOnlyDictionary<string, string> EuropeanTaxIdentifierPatterns = new Dictionary<string, string>
        {
            { "AT", "(AT)?U[0-9]{8}" },
            { "BE", "(BE)?0[0-9]{9}" },
            { "BG", "(BG)?[0-9]{9,10}" },
            { "CY", "(CY)?[0-9]{8}L" },
            { "CZ", "(CZ)?[0-9]{8,10}" },
            { "DE", "(DE)?[0-9]{9}" },
            { "DK", "(DK)?[0-9]{8}" },
            { "EE", "(EE)?[0-9]{9}" },
            { "GR", "(EL|GR)?[0-9]{9}" },
            { "ES", "(ES)?[0-9A-Z][0-9]{7}[0-9A-Z]" },
            { "FI", "(FI)?[0-9]{8}" },
            { "FR", "(FR)?[0-9A-Z]{2}[0-9]{9}" },
            { "GB", "(GB)?([0-9]{9}([0-9]{3})?|[A-Z]{2}[0-9]{3})" },
            { "HU", "(HU)?[0-9]{8}" },
            { "IE", "(IE)?[0-9]S[0-9]{5}L" },
            { "IT", "(IT)?[0-9]{11}" },
            { "LT", "(LT)?([0-9]{9}|[0-9]{12})" },
            { "LU", "(LU)?[0-9]{8}" },
            { "LV", "(LV)?[0-9]{11}" },
            { "MT", "(MT)?[0-9]{8}" },
            { "NL", "(NL)?[0-9]{9}B[0-9]{2}" },
            { "PL", "(PL)?[0-9]{10}" },
            { "PT", "(PT)?[0-9]{9}" },
            { "RO", "(RO)?[0-9]{2,10}" },
            { "SE", "(SE)?[0-9]{12}" },
            { "SI", "(SI)?[0-9]{8}" },
            { "SK", "(SK)?[0-9]{10}" },
        };
    }
}
