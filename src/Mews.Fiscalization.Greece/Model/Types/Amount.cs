using FuncSharp;

namespace Mews.Fiscalization.Greece.Model
{
    public sealed class Amount : Coproduct3<NonPositiveAmount, NonNegativeAmount, NegativeAmount>
    {
        public Amount(NonPositiveAmount nonPositiveAmount)
            : base(nonPositiveAmount)
        {
        }

        public Amount(NonNegativeAmount nonNegativeAmount)
            : base(nonNegativeAmount)
        {
        }

        public Amount(NegativeAmount negativeAmount)
            : base(negativeAmount)
        {
        }

        public decimal Value
        {
            get
            {
                return Match(
                    nonPositiveAmount => nonPositiveAmount.Value,
                    nonNegativeAmount => nonNegativeAmount.Value,
                    negativeAmount => negativeAmount.Value
                );
            }
        }
    }
}
