using Mews.Eet.Dto;

namespace Mews.Eet.Extensions
{
    public static class CurrencyValueExtensions
    {
        public static bool IsDefined(this CurrencyValue value)
        {
            return value != null;
        }

        public static decimal GetOrDefault(this CurrencyValue value)
        {
            if (!value.IsDefined())
            {
                return default(decimal);
            }

            return value.Value;
        }
    }
}