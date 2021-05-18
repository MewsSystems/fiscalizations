using System;
using Mews.Eet.Dto;

namespace Mews.Eet.Extensions
{
    public static class TaxRateItemExtensions
    {
        public static bool IsDefined(this TaxRateItem item)
        {
            return item != null;
        }

        public static bool IsValueDefined(this TaxRateItem item, Func<TaxRateItem, CurrencyValue> valueSelector)
        {
            return item.IsDefined() && valueSelector(item).IsDefined();
        }

        public static decimal GetOrDefault(this TaxRateItem item, Func<TaxRateItem, CurrencyValue> valueSelector)
        {
            return IsValueDefined(item, valueSelector) ? valueSelector(item).GetOrDefault() : default(decimal);
        }
    }
}