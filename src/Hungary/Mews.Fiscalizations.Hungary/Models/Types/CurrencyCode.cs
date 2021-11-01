using FuncSharp;
using Mews.Fiscalizations.Core.Model;
using System.Text.RegularExpressions;

namespace Mews.Fiscalizations.Hungary.Models
{
    public sealed class CurrencyCode
    {
        private CurrencyCode(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public override bool Equals(object other)
        {
            return other is CurrencyCode otherCurrencyCode && otherCurrencyCode.Value == Value;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static CurrencyCode HungarianForint()
        {
            return Create("HUF").GetUnsafe();
        }

        public static ITry<CurrencyCode, Error> Create(string value)
        {
            return StringValidations.NonEmptyNorWhitespace(value).FlatMap(v =>
            {
                var validCurrencyCode = StringValidations.RegexMatch(v, new Regex("[A-Z]{3}"));
                return validCurrencyCode.Map(c => new CurrencyCode(c));
            });
        }
    }
}
