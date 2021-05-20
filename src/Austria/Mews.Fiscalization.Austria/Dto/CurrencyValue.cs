using System;
using System.Globalization;

namespace Mews.Fiscalization.Austria.Dto
{
    public sealed class CurrencyValue
    {
        public CurrencyValue(decimal value)
        {
            var decimalPlaces = BitConverter.GetBytes(Decimal.GetBits(value)[3])[2];
            if (decimalPlaces > 2)
            {
                throw new ArgumentException("API requires the currency values to have at most 2 decimal places.");
            }

            var sanitizedValue = EnsureMinimalPrecision(value, decimalPlaces);

            Value = sanitizedValue;
            CurrencyIsoCode = "EUR";
        }

        public string CurrencyIsoCode { get; }

        public decimal Value { get; }

        public string FormatValue(CultureInfo culture)
        {
            return String.Format(culture, "{0:F2}", Value);
        }

        private decimal EnsureMinimalPrecision(decimal value, int placesCount)
        {
            switch (placesCount)
            {
                case 0:
                    return value * 1.00m;
                case 1:
                    return value * 1.0m;
                default:
                    return value;
            }
        }

        public static CurrencyValue Parse(string value, CultureInfo culture)
        {
            if (Decimal.TryParse(value, NumberStyles.Any, culture, out decimal val))
            {
                return new CurrencyValue(val);
            }
            throw new ArgumentException($"Value '{value}' is not a valid CurrencyValue.");
        }
    }
}
