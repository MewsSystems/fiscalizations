using System;
using FuncSharp;

namespace Mews.Fiscalizations.Core.Model
{
    public class NonEmptyNorWhitespaceString
    {
        private NonEmptyNorWhitespaceString(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static ITry<NonEmptyNorWhitespaceString, INonEmptyEnumerable<Error>> Create(string value)
        {
            return StringValidations.NonEmptyNorWhitespace(value).Map(v => new NonEmptyNorWhitespaceString(v));
        }

        public static NonEmptyNorWhitespaceString CreateUnsafe(string value)
        {
            return Create(value).GetUnsafe();
        }
    }
}