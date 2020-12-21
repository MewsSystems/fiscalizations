using System;
using FuncSharp;

namespace Mews.Fiscalization.Core.Model
{
    public class String1To50
    {
        private String1To50(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static ITry<String1To50, Error> Create(string value)
        {
            return StringValidations.LengthInRange(value, 1, 50).Map(v => new String1To50(v));
        }

        public static String1To50 CreateUnsafe(string value)
        {
            return Create(value).Get(error => new ArgumentException(error.Message));
        }
    }
}