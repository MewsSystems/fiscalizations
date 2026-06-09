using Mews.Fiscalizations.Core.Model;
using Mews.Fiscalizations.Germany.V2.Model;
using System.Globalization;

namespace Mews.Fiscalizations.Germany.V2;

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
        var groupedItems = bill.Items.GroupBy(i => i.VatRate).Select(g => new Item(g.Sum(i => i.Amount), g.Key));
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
            Amount = ToString(payment.Amount),
            CurrencyCode = payment.CurrencyCode,
            PaymentType = SerializePaymentType(payment.Type)
        };
    }

    private static Dto.AmountsPerVatRate SerializeItem(Item item)
    {
        return new Dto.AmountsPerVatRate
        {
            Amount = ToString(item.Amount),
            VatRate = SerializeVatRateType(item.VatRate)
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

    // SIGN DE's Kassenbeleg schema has a single bucket for the zero end, so the three canonical
    // zero/exempt classes (NotTaxable, TaxFree, NotDeterminable) all serialize to NULL.
    private static Dto.VatRateType SerializeVatRateType(GermanVatRate type)
    {
        return type.Match(
            GermanVatRate.Standard, _ => Dto.VatRateType.NORMAL,
            GermanVatRate.Reduced, _ => Dto.VatRateType.REDUCED_1,
            GermanVatRate.AverageHigher, _ => Dto.VatRateType.SPECIAL_RATE_1,
            GermanVatRate.AverageLower, _ => Dto.VatRateType.SPECIAL_RATE_2,
            GermanVatRate.NotTaxable, _ => Dto.VatRateType.NULL,
            GermanVatRate.TaxFree, _ => Dto.VatRateType.NULL,
            GermanVatRate.NotDeterminable, _ => Dto.VatRateType.NULL
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

    private static string ToString(decimal amount)
    {
        return String.Format(CultureInfo.InvariantCulture, "{0:F2}", amount);
    }
}