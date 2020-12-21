using System;
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

        public static ITry<NonEmptyString, Error> Create(string value)
        {
            return StringValidations.NonEmpty(value).Map(v => new NonEmptyString(v));
        }

        public static NonEmptyString CreateUnsafe(string value)
        {
            return Create(value).Get(error => new ArgumentException(error.Message));
        }
    }
}