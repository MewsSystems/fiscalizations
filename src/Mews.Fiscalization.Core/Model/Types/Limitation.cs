using System;

namespace Mews.Fiscalization.Core.Model
{
    public class Limitation<TValue>
        where TValue : struct, IComparable<TValue>
    {
        public Limitation(TValue? minimum = null, TValue? maximum = null)
        {
            Min = minimum;
            Max = maximum;
        }

        public TValue? Min { get; }

        public TValue? Max { get; }

        public virtual bool IsValid(TValue value)
        {
            return (!Min.HasValue || value.CompareTo(Min.Value) >= 0) && (!Max.HasValue || value.CompareTo(Max.Value) <= 0);
        }

        internal virtual void CheckValidity(TValue value, string label)
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