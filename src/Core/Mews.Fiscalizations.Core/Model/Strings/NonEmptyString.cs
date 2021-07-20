using FuncSharp;

namespace Mews.Fiscalizations.Core.Model
{
    public class NonEmptyString
    {
        private NonEmptyString(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static ITry<NonEmptyString, INonEmptyEnumerable<Error>> Create(string value)
        {
            return StringValidations.NonEmpty(value).Map(v => new NonEmptyString(v));
        }

        public static NonEmptyString CreateUnsafe(string value)
        {
            return Create(value).GetUnsafe();
        }
    }
}