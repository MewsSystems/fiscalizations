using FuncSharp;
using Mews.Fiscalization.Core.Model;

namespace Mews.Fiscalization.Spain.Model
{
    public sealed class Year
    {
        private Year(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public static ITry<Year, INonEmptyEnumerable<Error>> Create(int value)
        {
            return IntValidations.InRange(value, 1000, 10000).Map(v => new Year(v));
        }
    }
}
