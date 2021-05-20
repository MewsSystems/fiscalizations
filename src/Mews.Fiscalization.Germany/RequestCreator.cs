using Mews.Fiscalization.Germany.Model;
using System;
using System.Globalization;
using System.Linq;

namespace Mews.Fiscalization.Germany
{
    internal static class RequestCreator
    {
        internal static Dto.TransactionRequest CreateTransaction(Guid clientId)
        {
            return new Dto.TransactionRequest
            {
                ClientId = clientId,
                State = Dto.State.ACTIVE
            };
        }

        internal static Dto.CreateClientRequest CreateClient(string serialNumber)
        {
            return new Dto.CreateClientRequest
            {
                SerialNumber = serialNumber
            };
        }

        internal static Dto.CreateTssRequest CreateTss(TssState state, string description = "")
        {
            return new Dto.CreateTssRequest
            {
                Description = description,
                State = SerializeTssState(state)
            };
        }

        internal static Dto.UpdateTssRequest UpdateTss(TssState state)
        {
            return new Dto.UpdateTssRequest
            {
                State = SerializeTssState(state)
            };
        }

        internal static Dto.FinishTransactionRequest FinishTransaction(Guid clientId, Bill bill)
        {
            var groupedPayments = bill.Payments.GroupBy(p => new { p.CurrencyCode, p.Type }).Select(g => new Payment(g.Sum(p => p.Amount), g.Key.Type, g.Key.CurrencyCode));
            var groupedItems = bill.Items.GroupBy(i => i.VatRateType).Select(g => new Item(g.Sum(i => i.Amount), g.Key));
            return new Dto.FinishTransactionRequest
            {
                ClientId = clientId,
                State = Dto.State.FINISHED,
                Schema = new Dto.Schema
                {
                    StandardV1 = new Dto.StandardV1
                    {
                        Receipt = new Dto.Receipt
                        {
                            AmountsPerPaymentType = groupedPayments.Select(p => SerializePayment(p)).ToArray(),
                            AmountsPerVatRate = groupedItems.Select(i => SerializeItem(i)).ToArray(),
                            ReceiptType = SerializeBillType(bill.Type)
                        }
                    }
                }
            };
        }

        internal static Dto.AuthorizationTokenRequest CreateAuthorizationToken(ApiKey apiKey, ApiSecret apiSecret)
        {
            return new Dto.AuthorizationTokenRequest
            {
                ApiKey = apiKey.Value,
                ApiSecret = apiSecret.Value
            };
        }

        private static Dto.AmountsPerPaymentType SerializePayment(Payment payment)
        {
            return new Dto.AmountsPerPaymentType
            {
                Amount = (payment.Amount + 0.00m).ToString(CultureInfo.InvariantCulture),
                CurrencyCode = payment.CurrencyCode,
                PaymentType = SerializePaymentType(payment.Type)
            };
        }

        private static Dto.AmountsPerVatRate SerializeItem(Item item)
        {
            return new Dto.AmountsPerVatRate
            {
                Amount = (item.Amount + 0.00m).ToString(CultureInfo.InvariantCulture),
                VatRate = SerializeVatRateType(item.VatRateType)
            };
        }

        private static Dto.ReceiptType SerializeBillType(BillType type)
        {
            switch (type)
            {
                case BillType.Invoice:
                    return Dto.ReceiptType.INVOICE;
                case BillType.Receipt:
                    return Dto.ReceiptType.RECEIPT;
                default:
                    throw new NotImplementedException($"Receipt type: {type} is not implemented.");
            };
        }

        private static Dto.PaymentType SerializePaymentType(PaymentType type)
        {
            switch (type)
            {
                case PaymentType.Cash:
                    return Dto.PaymentType.CASH;
                case PaymentType.NonCash:
                    return Dto.PaymentType.NON_CASH;
                default:
                    throw new NotImplementedException($"Payment type: {type} is not implemented.");
            };
        }

        private static Dto.VatRateType SerializeVatRateType(VatRateType type)
        {
            switch (type)
            {
                case VatRateType.None:
                    return Dto.VatRateType.NULL;
                case VatRateType.Normal:
                    return Dto.VatRateType.NORMAL;
                case VatRateType.Reduced:
                    return Dto.VatRateType.REDUCED_1;
                case VatRateType.SpecialRate1:
                    return Dto.VatRateType.SPECIAL_RATE_1;
                case VatRateType.SpecialRate2:
                    return Dto.VatRateType.SPECIAL_RATE_2;
                default:
                    throw new NotImplementedException($"Vat rate type: {type} is not implemented.");
            };
        }

        private static Dto.TssState SerializeTssState(TssState state)
        {
            switch (state)
            {
                case TssState.Disabled:
                    return Dto.TssState.DISABLED;
                case TssState.Initialized:
                    return Dto.TssState.INITIALIZED;
                case TssState.Uninitialized:
                    return Dto.TssState.UNINITIALIZED;
                default:
                    throw new NotImplementedException($"Tss state: {state} is not implemented.");
            };
        }
    }
}
