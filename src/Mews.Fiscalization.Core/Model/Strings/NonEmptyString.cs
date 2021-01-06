using System;
using System.Collections.Generic;
using System.Linq;
using FuncSharp;

namespace Mews.Fiscalization.Core.Model
{
    public class NonEmptyString
    {
        private NonEmptyString(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static ITry<NonEmptyString, IEnumerable<Error>> Create(string value)
        {
            return StringValidations.NonEmpty(value).Map(v => new NonEmptyString(v));
        }

        public static NonEmptyString CreateUnsafe(string value)
        {
            return Create(value).Get(errors => new ArgumentException(errors.Select(e => e.Message).MkString()));
        }
    }
}