﻿using FuncSharp;
using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Greece.Model
{
    public sealed class NonPositiveAmount
    {
        private NonPositiveAmount(decimal value)
        {
            Value = value;
        }

        public decimal Value { get; }

        public static ITry<NonPositiveAmount, INonEmptyEnumerable<Error>> Create(decimal value)
        {
            return DecimalValidations.MaxDecimalPlaces(value, 2).FlatMap(v =>
            {
                var validNumber = DecimalValidations.SmallerThanOrEqual(v, 0);
                return validNumber.Map(n => new NonPositiveAmount(n));
            });
        }
    }
}
