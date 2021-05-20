using FuncSharp;
using Mews.Fiscalization.Core.Model;

namespace Mews.Fiscalization.Greece.Model
{
    public sealed class NonNegativePayment
    {
        private NonNegativePayment(NonNegativeAmount amount, PaymentType paymentType)
        {
            Amount = amount;
            PaymentType = paymentType;
        }

        public NonNegativeAmount Amount { get; }

        public PaymentType PaymentType { get; }

        public static ITry<NonNegativePayment, INonEmptyEnumerable<Error>> Create(NonNegativeAmount amount, PaymentType paymentType)
        {
            return ObjectValidations.NotNull(amount).Map(a => new NonNegativePayment(a, paymentType));
        }
    }
}
