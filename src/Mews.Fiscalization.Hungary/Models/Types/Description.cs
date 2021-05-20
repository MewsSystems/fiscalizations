using FuncSharp;
using Mews.Fiscalization.Core.Model;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class Description
    {
        private Description(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static ITry<Description, INonEmptyEnumerable<Error>> Create(string value)
        {
            return ValidationExtensions.ValidateString(value, minLength: 1, maxLength: 512, regex: ".*[^\\s].*").Map(v => new Description(v));
        }
    }
}
