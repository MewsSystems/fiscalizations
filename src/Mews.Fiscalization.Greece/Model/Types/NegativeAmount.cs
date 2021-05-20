using FuncSharp;
using Mews.Fiscalization.Core.Model;

namespace Mews.Fiscalization.Greece.Model
{
    public sealed class NegativeAmount
    {
        private NegativeAmount(decimal value)
        {
            Value = value;
        }

        public decimal Value { get; }

        public static ITry<NegativeAmount, INonEmptyEnumerable<Error>> Create(decimal value)
        {
            return DecimalValidations.MaxDecimalPlaces(value, 2).FlatMap(v =>
            {
                var validNumber = DecimalValidations.SmallerThan(v, 0);
                return validNumber.Map(n => new NegativeAmount(n));
            });
        }
    }
}

