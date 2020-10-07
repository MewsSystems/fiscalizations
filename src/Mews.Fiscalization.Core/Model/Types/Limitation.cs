using System;

namespace Mews.Fiscalization.Core.Model
{
    public sealed class Limitation<TValue>
        where TValue : struct, IComparable<TValue>
    {
        private Limitation(TValue? minimum = null, TValue? maximum = null)
        {
            Min = minimum;
            Max = maximum;
        }

        public TValue? Min { get; }

        public TValue? Max { get; }

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

        public bool IsValid(TValue value)
        {
            return (!Min.HasValue || value.CompareTo(Min.Value) >= 0) && (!Max.HasValue || value.CompareTo(Max.Value) <= 0);
        }

        public static Limitation<T> Create<T>(T minimum, T maximum)
            where T : struct, IComparable<T>
        {
            return new Limitation<T>(minimum, maximum);
        }

        public static Limitation<T> Minimum<T>(T minimum)
            where T : struct, IComparable<T>
        {
            return new Limitation<T>(minimum: minimum);
        }

        public static Limitation<T> Maximum<T>(T maximum)
            where T : struct, IComparable<T>
        {
            return new Limitation<T>(maximum: maximum);
        }
    }
}