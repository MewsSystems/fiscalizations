using FuncSharp;
using Mews.Fiscalization.Core.Model;

namespace Mews.Fiscalization.Spain.Model
{
    public sealed class Percentage
    {
        private Percentage(decimal value)
        {
            Value = value;
        }

        public decimal Value { get; }

        public static ITry<Percentage, INonEmptyEnumerable<Error>> Create(decimal value)
        {
            return DecimalValidations.InRange(value, 0, 100).FlatMap(v =>
            {
                var validatedValue = DecimalValidations.MaxDecimalPlaces(v, 2);
                return validatedValue.Map(p => new Percentage(p));
            });
        }
    }
}
