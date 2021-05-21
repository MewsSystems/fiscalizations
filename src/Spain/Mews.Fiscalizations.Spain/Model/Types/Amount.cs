using FuncSharp;
using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Spain.Model
{
    public sealed class Amount
    {
        private Amount(decimal value)
        {
            Value = value;
        }

        public decimal Value { get; }

        public static ITry<Amount, INonEmptyEnumerable<Error>> Create(decimal value)
        {
            return DecimalValidations.SmallerThan(value, 1000000000000).FlatMap(v =>
            {
                var validatedValue = DecimalValidations.MaxDecimalPlaces(v, 2);
                return validatedValue.Map(a => new Amount(a));
            });
        }
    }
}
