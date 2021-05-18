using System;

namespace Mews.Eet.Dto
{
    public class CurrencyValue
    {
        public CurrencyValue(decimal value)
        {
            var decimalPlaces = BitConverter.GetBytes(Decimal.GetBits(value)[3])[2];
            if (decimalPlaces > 2)
            {
                throw new ArgumentException("EET requires the currency values to have at most 2 decimal places.");
            }

            var sanitizedValue = EnsureMinimalPrecision(value, decimalPlaces);
            if (sanitizedValue > 99999999.99m)
            {
                throw new ArgumentException("The value cannot be higher than 99 999 999,99 Kč.");
            }

            if (sanitizedValue < -99999999.99m)
            {
                throw new ArgumentException("The value cannot be lower than -99 999 999,99 Kč.");
            }

            Value = sanitizedValue;
            CurrencyIsoCode = "CZK";
        }

        public string CurrencyIsoCode { get; }

        public decimal Value { get; }

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
    }
}