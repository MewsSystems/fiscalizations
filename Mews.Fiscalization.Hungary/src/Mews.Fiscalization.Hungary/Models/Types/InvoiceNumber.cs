using FuncSharp;
using Mews.Fiscalization.Core.Model;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class InvoiceNumber
    {
        private InvoiceNumber(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static ITry<InvoiceNumber, INonEmptyEnumerable<Error>> Create(string value)
        {
            return ValidationExtensions.ValidateString(value, minLength: 1, maxLength: 50, regex: ".*[^\\s].*").Map(v => new InvoiceNumber(v));
        }
    }
}
