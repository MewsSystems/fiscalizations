using FuncSharp;

namespace Mews.Fiscalizations.Core.Model
{
    public sealed class String1To250
    {
        private String1To250(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static ITry<String1To250, Error> Create(string value)
        {
            return StringValidations.LengthInRange(value, 1, 250).Map(v => new String1To250(v));
        }

        public static String1To250 CreateUnsafe(string value)
        {
            return Create(value).GetUnsafe();
        }
    }
}
