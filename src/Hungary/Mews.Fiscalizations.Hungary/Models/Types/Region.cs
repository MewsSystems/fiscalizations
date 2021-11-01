using FuncSharp;
using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Hungary.Models
{
    public sealed class Region
    {
        private Region(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static ITry<Region, Error> Create(string value)
        {
            return ValidationExtensions.ValidateString(value, minLength: 0, maxLength: 50, regex: ".*[^\\s].*").Map(v => new Region(v));
        }
    }
}
