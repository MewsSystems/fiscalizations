﻿namespace Mews.Fiscalization.Core.Model
{
    public abstract class LimitedString
    {
        protected LimitedString(string value, StringLimitation limitation)
        {
            limitation.CheckValidity(value);
            Value = value;
        }

        public string Value { get; }

        protected static bool IsValid(string value, StringLimitation limitation)
        {
            return limitation.IsValid(value);
        }
    }
}
