using FuncSharp;
using Mews.Fiscalization.Core.Model;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class Name
    {
        private Name(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static ITry<Name, INonEmptyEnumerable<Error>> Create(string value)
        {
            return ValidationExtensions.ValidateString(value, minLength: 1, maxLength: 512, regex: ".*[^\\s].*").Map(v => new Name(v));
        }
    }
}
