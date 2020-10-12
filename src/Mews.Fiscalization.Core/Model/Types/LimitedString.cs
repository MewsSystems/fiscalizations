﻿namespace Mews.Fiscalization.Core.Model
{
    public abstract class LimitedString : ValueWrapper<string>
    {
        protected LimitedString(string value, StringLimitation limitation)
            : base(value)
        {
            limitation.CheckValidity(value);
        }

        protected static bool IsValid(string value, StringLimitation limitation)
        {
            return limitation.IsValid(value);
        }
    }
}
