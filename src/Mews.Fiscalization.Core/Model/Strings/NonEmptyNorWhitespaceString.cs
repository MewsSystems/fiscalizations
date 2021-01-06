using System;
using System.Collections.Generic;
using System.Linq;
using FuncSharp;

namespace Mews.Fiscalization.Core.Model
{
    public class NonEmptyNorWhitespaceString
    {
        private NonEmptyNorWhitespaceString(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static ITry<NonEmptyNorWhitespaceString, IEnumerable<Error>> Create(string value)
        {
            return StringValidations.NonEmptyNorWhitespace(value).Map(v => new NonEmptyNorWhitespaceString(v));
        }

        public static NonEmptyNorWhitespaceString CreateUnsafe(string value)
        {
            return Create(value).Get(errors => new ArgumentException(errors.Select(e => e.Message).MkString(",")));
        }
    }
}