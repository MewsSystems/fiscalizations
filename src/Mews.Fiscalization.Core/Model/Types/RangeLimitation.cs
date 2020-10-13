using System;

namespace Mews.Fiscalization.Core.Model
{
    public class RangeLimitation<TValue> : ILimitation<TValue>
        where TValue : struct, IComparable<TValue>
    {
        public RangeLimitation(TValue? min = null, TValue? max = null, bool minIsAllowed = true, bool maxIsAllowed = true)
        {
            Min = min;
            Max = max;
            MinIsAllowed = minIsAllowed;
            MaxIsAllowed = maxIsAllowed;
        }

        private TValue? Min { get; }

        private TValue? Max { get; }

        private bool MinIsAllowed { get; }

        private bool MaxIsAllowed { get; }

        public bool IsValid(TValue value)
        {
            return FitsMin(value) && FitsMax(value);
        }

        public void CheckValidity(TValue value)
        {
            CheckValidity(value, "value");
        }

        internal void CheckValidity(TValue value, string label)
        {
            Check.Condition(FitsMin(value), $"Min {label} is {Min}.");
            Check.Condition(FitsMax(value), $"Max {label} is {Max}.");
        }

        private bool FitsMin(TValue value)
        {
            return !Min.HasValue || value.CompareTo(Min.Value) > 0 || (MinIsAllowed && value.CompareTo(Min.Value) == 0);
        }

        private bool FitsMax(TValue value)
        {
            return !Max.HasValue || value.CompareTo(Max.Value) < 0 || (MaxIsAllowed && value.CompareTo(Max.Value) == 0);
        }
    }
}