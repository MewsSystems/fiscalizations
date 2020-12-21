using System;
using FuncSharp;

namespace Mews.Fiscalization.Core.Model
{
    public class NonEmptyOrWhitespaceString
    {
        private NonEmptyOrWhitespaceString(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static ITry<NonEmptyOrWhitespaceString, Error> Create(string value)
        {
            return StringValidations.NonEmptyOrWhitespace(value).Map(v => new NonEmptyOrWhitespaceString(v));
        }

        public static NonEmptyOrWhitespaceString CreateUnsafe(string value)
        {
            return Create(value).Get(error => new ArgumentException(error.Message));
        }
    }
}