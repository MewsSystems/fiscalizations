using FuncSharp;
using Mews.Fiscalization.Core.Model;

namespace Mews.Fiscalization.Greece.Model
{
    public class NonNegativeAmount
    {
        private NonNegativeAmount(decimal value)
        {
            Value = value;
        }

        public decimal Value { get; }

        public static ITry<NonNegativeAmount, INonEmptyEnumerable<Error>> Create(decimal value)
        {
            return DecimalValidations.MaxDecimalPlaces(value, 2).FlatMap(v =>
            {
                var validNumber = DecimalValidations.HigherThanOrEqual(v, 0);
                return validNumber.Map(n => new NonNegativeAmount(n));
            });
        }
    }
}
