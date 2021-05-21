using System;
using System.Linq;
using FuncSharp;

namespace Mews.Fiscalizations.Core.Model
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

        public static ITry<NonNegativeInt, INonEmptyEnumerable<Error>> Create(int value)
        {
            return IntValidations.HigherThanOrEqual(value, 0).Map(v => new NonNegativeInt(v));
        }

        public static NonNegativeInt CreateUnsafe(int value)
        {
            return Create(value).GetUnsafe();
        }

        public static NonNegativeInt Zero()
        {
            return CreateUnsafe(0);
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