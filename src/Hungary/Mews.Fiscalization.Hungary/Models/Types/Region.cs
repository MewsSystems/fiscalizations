using FuncSharp;
using Mews.Fiscalization.Core.Model;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class Region
    {
        private Region(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static ITry<Region, INonEmptyEnumerable<Error>> Create(string value)
        {
            return ValidationExtensions.ValidateString(value, minLength: 0, maxLength: 50, regex: ".*[^\\s].*").Map(v => new Region(v));
        }
    }
}
