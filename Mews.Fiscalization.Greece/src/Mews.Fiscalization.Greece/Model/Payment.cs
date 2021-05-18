using FuncSharp;

namespace Mews.Fiscalization.Greece.Model
{
    public sealed class Payment : Coproduct2<NonNegativePayment, NegativePayment>
    {
        public Payment(NonNegativePayment nonNegativePayment)
            : base(nonNegativePayment)
        {
        }

        public Payment(NegativePayment negativePayment)
            : base(negativePayment)
        {
        }

        public decimal Amount
        {
            get
            {
                return Match(
                    nonNegativePayment => nonNegativePayment.Amount.Value,
                    negativePayment => negativePayment.Amount.Value
                );
            }
        }

        public PaymentType PaymentType
        {
            get
            {
                return Match(
                    nonNegativePayment => nonNegativePayment.PaymentType,
                    negativePayment => negativePayment.PaymentType
                );
            }
        }
    }
}
