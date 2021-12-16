using FuncSharp;
using Mews.Fiscalizations.Core.Model;
using System;

namespace Mews.Fiscalizations.Hungary.Models
{
    public sealed class ExchangeRate
    {
        private static readonly int MaxDecimalPlaces = 6;

        private ExchangeRate(decimal value)
        {
            Value = value;
        }

        public decimal Value { get; }

        public static ITry<ExchangeRate, Error> Create(decimal value)
        {
            return DecimalValidations.InRange(value, 0, 100_000_000, minIsAllowed: false, maxIsAllowed: false).FlatMap(v =>
            {
                var validExchangeRate = DecimalValidations.MaxDecimalPlaces(v, MaxDecimalPlaces);
                return validExchangeRate.Map(r => new ExchangeRate(r));
            });
        }

        internal static ITry<ExchangeRate, Error> Rounded(decimal value)
        {
            var roundedValue = Decimal.Round(value, MaxDecimalPlaces);
            return Create(roundedValue);
        }
    }
}