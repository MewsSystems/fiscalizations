using System;

namespace Mews.Fiscalization.Core.Model
{
    public sealed class RangeLimitation<TValue>
        where TValue : struct, IComparable<TValue>
    {
        public RangeLimitation(TValue? min = null, TValue? max = null)
        {
            Min = min;
            Max = max;
        }

        private TValue? Min { get; }

        private TValue? Max { get; }

        public bool IsValid(TValue value)
        {
            return (!Min.HasValue || value.CompareTo(Min.Value) >= 0) && (!Max.HasValue || value.CompareTo(Max.Value) <= 0);
        }

        internal void CheckValidity(TValue value, string label)
        {
            if (Min.HasValue && value.CompareTo(Min.Value) < 0)
            {
                throw new ArgumentException($"Min {label} is {Min}.");
            }

            if (Max.HasValue && value.CompareTo(Max.Value) > 0)
            {
                throw new ArgumentException($"Max {label} is {Max}.");
            }
        }
    }
}