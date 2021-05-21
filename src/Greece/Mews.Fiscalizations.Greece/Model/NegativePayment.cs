using FuncSharp;
using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Greece.Model
{
    public sealed class NegativePayment
    {
        private NegativePayment(NegativeAmount amount, PaymentType paymentType)
        {
            Amount = amount;
            PaymentType = paymentType;
        }

        public NegativeAmount Amount { get; }

        public PaymentType PaymentType { get; }

        public static ITry<NegativePayment, INonEmptyEnumerable<Error>> Create(NegativeAmount amount, PaymentType paymentType)
        {
            return ObjectValidations.NotNull(amount).Map(a => new NegativePayment(a, paymentType));
        }
    }
}
