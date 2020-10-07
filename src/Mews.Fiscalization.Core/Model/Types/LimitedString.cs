﻿namespace Mews.Fiscalization.Core.Model
{
    public abstract class LimitedString
    {
        protected LimitedString(string value, Limitation<int> limitation)
        {
            limitation.CheckValidity(value.Length, label: "length of string");
            Value = value;
        }

        public string Value { get; }

        protected static bool IsValid(string value, Limitation<int> limitation)
        {
            return limitation.IsValid(value.Length);
        }
    }
}
