using System;
using System.Linq;
using FuncSharp;

namespace Mews.Fiscalization.Core.Model
{
    public struct NonNegativeInt
    {
        private NonNegativeInt(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public static implicit operator int(NonNegativeInt i)
        {
            return i.Value;
        }

        public static NonNegativeInt operator +(NonNegativeInt a, NonNegativeInt b)
        {
            return a.Sum(b);
        }

        public static ITry<NonNegativeInt, string> Create(int value)
        {
            return IntValidations.HigherThanOrEqual(value, 0).Map(v => new NonNegativeInt(v));
        }

        public static NonNegativeInt CreateUnsafe(int value)
        {
            return Create(value).Get(errorMessage => new ArgumentException(errorMessage));
        }

        public NonNegativeInt Sum(params NonNegativeInt[] values)
        {
            return new NonNegativeInt(values.Aggregate(Value, (a, b) => a + b));
        }

        public NonNegativeInt Multiply(params NonNegativeInt[] values)
        {
            return new NonNegativeInt(values.Aggregate(Value, (a, b) => a * b));
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}