﻿using FuncSharp;
using Mews.Fiscalization.Core.Model;

namespace Mews.Fiscalization.Spain.Model
{
    public sealed class String0To500
    {
        private String0To500(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static ITry<String0To500, INonEmptyEnumerable<Error>> Create(string value)
        {
            return StringValidations.LengthInRange(value, 0, 500).Map(v => new String0To500(v));
        }

        public static String0To500 CreateUnsafe(string value)
        {
            return Create(value).GetUnsafe();
        }
    }
}
