using FuncSharp;
using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Spain.Model
{
    public sealed class String1To20
    {
        private String1To20(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static ITry<String1To20, Error> Create(string value)
        {
            return StringValidations.LengthInRange(value, 1, 20).Map(v => new String1To20(v));
        }

        public static String1To20 CreateUnsafe(string value)
        {
            return Create(value).GetUnsafe();
        }
    }
}
