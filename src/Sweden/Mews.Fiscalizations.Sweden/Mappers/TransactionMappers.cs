using System.Globalization;
using System.Text.RegularExpressions;
using FuncSharp;
using Mews.Fiscalizations.Sweden.Models;

namespace Mews.Fiscalizations.Sweden.Mappers;

internal static class TransactionMappers
{
    private const string DateTimeFormat = "yyyyMMddHHmmss";
    private static readonly CultureInfo Culture = new("fr-FR");

    internal static TransactionResponse FromDto(this DTOs.TcsResponse response, string requestXml)
    {
        return new TransactionResponse(
            requestXml: requestXml,
            controlServerId: response.ControlCode.ControlServerId,
            controlCode: response.ControlCode.Code,
            sequenceNumber: response.SequenceNumber,
            skvResponseCode: response.SKVResponseCode,
            skvResponseMessage: response.SKVResponseMessage,
            applicationId: response.ApplicationId,
            responseCode: response.ResponseCode,
            responseMessage: response.ResponseMessage,
            requestId: response.RequestId,
            responseReason: response.ResponseReason
        );
    }

    internal static DTOs.TcsRequest ToDto(this TransactionData data, string applicationId, Guid? requestId)
    {
        return new DTOs.TcsRequest
        {
            ApplicationID = applicationId,
            RequestID = requestId ?? Guid.NewGuid(),
            ControlData = new DTOs.ControlData
            {
                DateTime = data.DateTime.ToString(DateTimeFormat),
                OrgNr = data.OrganizationNumber,
                ManRegisterID = data.OrganizationRegisterID,
                RegisterAddress = data.RegisterFullAddress,
                SequenceNumber = data.SequenceNumber,
                ReceiptType = new DTOs.ReceiptType
                {
                    Type = data.TransactionType.ToDto()
                },
                Vat25 = data.TwentyFivePercentTax.ToDto(25),
                Vat12 = data.TwelvePercentTax.ToDto(12),
                Vat6 = data.SixPercentTax.ToDto(6),
                Vat0 = data.ZeroPercentTax.ToDto(0),
                SaleAmount = data.SaleAmount.GetOrNull(a => ToAmountString(a)),
                RefundAmount = data.RefundAmount.GetOrNull(a => ToAmountString(a)),
                CopyDateTime = data.CopyDateTime.GetOrNull(t => t.ToString(DateTimeFormat)),
                CopySequenceNumber = data.CopySequenceNumber.ToNullable()
            }
        };
    }

    private static DTOs.VAT ToDto(this TaxAmount? taxAmount, decimal percent)
    {
        return new DTOs.VAT
        {
            Percent = percent.ToString("F2", Culture),
            Amount = ToAmountString(taxAmount?.Amount),
            SubtotalAmount = ToAmountString(taxAmount?.SubtotalAmount)
        };
    }

    private static DTOs.ReceiptOperationType ToDto(this TransactionType transactionType)
    {
        return transactionType switch
        {
            TransactionType.Sale => DTOs.ReceiptOperationType.Normal,
            TransactionType.Copy => DTOs.ReceiptOperationType.Copy,
            TransactionType.Proforma => DTOs.ReceiptOperationType.Proforma,
            TransactionType.Training => DTOs.ReceiptOperationType.Training,
            _ => throw new NotImplementedException($"Transaction type {transactionType} is not supported.")
        };
    }

    private static string ToAmountString(decimal? amount)
    {
        var value = amount?.ToString("N2", Culture) ?? "0,00";
        return Regex.Replace(value, @"\s+", "");
    }
}