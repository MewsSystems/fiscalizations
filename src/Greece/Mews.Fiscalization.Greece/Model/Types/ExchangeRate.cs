using FuncSharp;
using Mews.Fiscalization.Core.Model;

namespace Mews.Fiscalization.Greece.Model
{
    public sealed class ExchangeRate
    {
        private ExchangeRate(decimal value)
        {
            Value = value;
        }

        public decimal Value { get; }

        public static ITry<ExchangeRate, INonEmptyEnumerable<Error>> Create(decimal value)
        {
            return DecimalValidations.MaxDecimalPlaces(value, 5).FlatMap(rate =>
            {
                var validExchangeRate = DecimalValidations.HigherThanOrEqual(rate, 0);
                return validExchangeRate.Map(r => new ExchangeRate(r));
            });
        }
    }
}
