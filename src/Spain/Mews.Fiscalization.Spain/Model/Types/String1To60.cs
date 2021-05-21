using FuncSharp;
using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Spain.Model
{
    public sealed class String1To60
    {
        private String1To60(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static ITry<String1To60, INonEmptyEnumerable<Error>> Create(string value)
        {
            return StringValidations.LengthInRange(value, 1, 60).Map(v => new String1To60(v));
        }

        public static String1To60 CreateUnsafe(string value)
        {
            return Create(value).GetUnsafe();
        }
    }
}
