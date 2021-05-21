using Mews.Fiscalizations.Greece.Dto.Xsd;
using System;
using System.Collections.Generic;
using System.Linq;
using Mews.Fiscalizations.Core.Model;
using FuncSharp;

namespace Mews.Fiscalizations.Greece.Model
{
    public sealed class CurrencyCode
    {
        private static readonly IEnumerable<string> AllowedValues = Enum.GetValues(typeof(Currency)).OfType<Currency>().Select(v => v.ToString());

        private CurrencyCode(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static ITry<CurrencyCode, INonEmptyEnumerable<Core.Model.Error>> Create(string value)
        {
            return StringValidations.In(value, AllowedValues).Map(v => new CurrencyCode(v));
        }
    }
}

