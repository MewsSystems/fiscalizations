using FuncSharp;
using Mews.Fiscalization.Core.Model;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class Login
    {
        private Login(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static ITry<Login, INonEmptyEnumerable<Error>> Create(string value)
        {
            return ValidationExtensions.ValidateString(value, minLength: 1, maxLength: 15, regex: "^[0-9A-Za-z]{15}$").Map(v => new Login(v));
        }
    }
}
