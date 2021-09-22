using FuncSharp;
using Mews.Fiscalizations.Germany.Model;
using System;
using System.Globalization;
using System.Linq;

namespace Mews.Fiscalizations.Germany
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

        internal static Dto.UpdateClientRequest UpdateClient(ClientState state)
        {
            return new Dto.UpdateClientRequest
            {
                State = SerializeClientState(state)
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

        internal static Dto.AdminLoginRequest AdminLoginRequest(string adminPin)
        {
            return new Dto.AdminLoginRequest
            {
                AdminPin = adminPin
            };
        }

        internal static Dto.AdminSetPinRequest CreateAdminSetPinRequest(string adminPuk, string newAdminPin)
        {
            return new Dto.AdminSetPinRequest
            {
                AdminPuk = adminPuk,
                NewAdminPin = newAdminPin
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
            return type.Match(
                BillType.Invoice, _ => Dto.ReceiptType.INVOICE,
                BillType.Receipt, _ => Dto.ReceiptType.RECEIPT
            );
        }

        private static Dto.PaymentType SerializePaymentType(PaymentType type)
        {
            return type.Match(
                PaymentType.Cash, _ => Dto.PaymentType.CASH,
                PaymentType.NonCash, _ => Dto.PaymentType.NON_CASH
            );
        }

        private static Dto.VatRateType SerializeVatRateType(VatRateType type)
        {
            return type.Match(
                VatRateType.None, _ => Dto.VatRateType.NULL,
                VatRateType.Normal, _ => Dto.VatRateType.NORMAL,
                VatRateType.Reduced, _ => Dto.VatRateType.REDUCED_1,
                VatRateType.SpecialRate1, _ => Dto.VatRateType.SPECIAL_RATE_1,
                VatRateType.SpecialRate2, _ => Dto.VatRateType.SPECIAL_RATE_2
            );
        }

        private static Dto.TssState SerializeTssState(TssState state)
        {
            return state.Match(
                TssState.Disabled, _ => Dto.TssState.DISABLED,
                TssState.Initialized, _ => Dto.TssState.INITIALIZED,
                TssState.Uninitialized, _ => Dto.TssState.UNINITIALIZED
            );
        }

        private static Dto.ClientState SerializeClientState(ClientState state)
        {
            return state.Match(
                ClientState.Registered, _ => Dto.ClientState.REGISTERED,
                ClientState.Deregistered, _ => Dto.ClientState.DEREGISTERED
            );
        }
    }
}
