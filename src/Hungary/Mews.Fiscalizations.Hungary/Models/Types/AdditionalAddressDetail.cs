using FuncSharp;
using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Hungary.Models
{
    public sealed class AdditionalAddressDetail
    {
        private AdditionalAddressDetail(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static ITry<AdditionalAddressDetail, INonEmptyEnumerable<Error>> Create(string value)
        {
            return ValidationExtensions.ValidateString(value, minLength: 1, maxLength: 255, regex: ".*[^\\s].*").Map(v => new AdditionalAddressDetail(v));
        }
    }
}
