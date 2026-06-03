using System.Globalization;
using Mews.Fiscalizations.Fiskaly.Models.DSFinVK.CashPointClosings;
using AmountPerVatDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.AmountPerVat;
using BusinessCaseDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.BusinessCase;
using BusinessCaseSummaryDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.BusinessCaseSummary;
using CashAmountByCurrencyDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.CashAmountByCurrency;
using CashPointClosingHeadDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.CashPointClosingHead;
using CashPointClosingStateDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.CashPointClosingState;
using CashPointClosingTransactionDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.CashPointClosingTransaction;
using CashStatementDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.CashStatement;
using CashStatementPaymentDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.CashStatementPayment;
using InsertCashPointClosingRequestDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.InsertCashPointClosingRequest;
using PaymentTypeAmountDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.PaymentTypeAmount;
using TransactionDataDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.TransactionData;
using TransactionHeadDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.TransactionHead;
using TransactionLineDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.TransactionLine;
using TransactionLineItemDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.TransactionLineItem;
using TransactionReferenceDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.TransactionReference;
using TransactionSecurityDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.TransactionSecurity;
using CashPointClosingResponseDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.CashPointClosingResponse;

namespace Mews.Fiscalizations.Fiskaly.Mappers.DSFinVK.CashPointClosings;

internal static class CashPointClosingMapper
{
    public static InsertCashPointClosingRequestDto MapInsertRequest(CashPointClosing closing)
    {
        var transactions = closing.Transactions.ToList();

        return new InsertCashPointClosingRequestDto
        {
            ClientId = closing.ClientId,
            CashPointClosingExportId = closing.CashPointClosingExportId,
            Head = new CashPointClosingHeadDto
            {
                ExportCreationDate = closing.ExportCreationDate.ToUnixTimeSeconds(),
                BusinessDate = closing.BusinessDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                FirstTransactionExportId = transactions.FirstOrDefault()?.TransactionExportId ?? "0",
                LastTransactionExportId = transactions.LastOrDefault()?.TransactionExportId ?? "0"
            },
            Transactions = transactions.Select(MapTransaction).ToList(),
            CashStatement = MapCashStatement(closing.CashStatement)
        };
    }

    public static CashPointClosingResult MapResponse(this CashPointClosingResponseDto response)
    {
        return new CashPointClosingResult(
            Id: response.Id,
            ClientId: response.ClientId,
            CashPointClosingExportId: response.CashPointClosingExportId,
            State: MapState(response.State),
            Error: response.Error
        );
    }

    private static CashPointClosingTransactionDto MapTransaction(CashPointClosingTransaction tx)
    {
        return new CashPointClosingTransactionDto
        {
            Head = new TransactionHeadDto
            {
                TxId = tx.TxId,
                TransactionExportId = tx.TransactionExportId,
                Number = tx.Number,
                TimestampStart = tx.TimestampStart.ToUnixTimeSeconds(),
                TimestampEnd = tx.TimestampEnd.ToUnixTimeSeconds(),
                Storno = tx.Storno,
                Type = MapProcessType(tx.ProcessType),
                ClosingClientId = tx.ClosingClientId,
                References = tx.References?.Select(MapReference).ToList(),
                AllocationGroups = tx.AllocationGroups?.ToList()
            },
            Data = new TransactionDataDto
            {
                FullAmountInclVat = tx.FullAmountInclVat,
                Lines = tx.Lines.Select(MapLine).ToList(),
                AmountsPerVatId = tx.AmountsPerVat.Select(MapAmountPerVat).ToList(),
                PaymentTypes = tx.PaymentTypes.Select(MapPaymentType).ToList()
            },
            Security = new TransactionSecurityDto
            {
                TssTxId = tx.Security.TssTxId,
                ErrorMessage = tx.Security.ErrorMessage
            }
        };
    }

    private static TransactionReferenceDto MapReference(TransactionReference reference)
    {
        return new TransactionReferenceDto
        {
            Type = MapReferenceType(reference.Type),
            TxId = reference.TxId,
            CashPointClosingExportId = reference.CashPointClosingExportId,
            CashRegisterExportId = reference.CashRegisterExportId,
            TransactionExportId = reference.TransactionExportId,
            ExternalExportId = reference.ExternalExportId,
            ExternalOtherExportId = reference.ExternalOtherExportId,
            Name = reference.Name,
            Date = reference.Date?.ToUnixTimeSeconds()
        };
    }

    private static string MapReferenceType(ReferenceType type)
    {
        return type switch
        {
            ReferenceType.InternalTransaction => "InterneTransaktion",
            ReferenceType.Transaction => "Transaktion",
            ReferenceType.ExternalBill => "ExterneRechnung",
            ReferenceType.ExternalDeliveryNote => "ExternerLieferschein",
            ReferenceType.ExternalOther => "ExterneSonstige",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    private static TransactionLineDto MapLine(TransactionLine line)
    {
        return new TransactionLineDto
        {
            LineItemExportId = line.LineItemExportId,
            Storno = line.Storno,
            BusinessCase = new BusinessCaseDto
            {
                Type = MapBusinessTransactionType(line.BusinessTransactionType),
                AmountsPerVatId = line.BusinessCaseAmountsPerVat.Select(MapAmountPerVat).ToList()
            },
            ItemText = line.ItemText,
            Item = line.Item == null ? null : new TransactionLineItemDto
            {
                Number = line.Item.Number,
                Quantity = line.Item.Quantity,
                PricePerUnit = line.Item.PricePerUnit
            }
        };
    }

    private static AmountPerVatDto MapAmountPerVat(AmountPerVat amount)
    {
        return new AmountPerVatDto
        {
            VatDefinitionExportId = amount.VatDefinitionExportId,
            GrossAmount = amount.GrossAmount,
            NetAmount = amount.NetAmount,
            TaxAmount = amount.TaxAmount
        };
    }

    private static PaymentTypeAmountDto MapPaymentType(PaymentTypeAmount payment)
    {
        return new PaymentTypeAmountDto
        {
            PaymentType = payment.PaymentType,
            Amount = payment.Amount,
            CurrencyCode = payment.CurrencyCode
        };
    }

    private static CashStatementDto MapCashStatement(CashStatement cashStatement)
    {
        return new CashStatementDto
        {
            BusinessCases = cashStatement.BusinessCases.Select(bc => new BusinessCaseSummaryDto
            {
                Type = MapBusinessTransactionType(bc.Type),
                AmountsPerVatId = bc.AmountsPerVat.Select(MapAmountPerVat).ToList()
            }).ToList(),
            Payment = new CashStatementPaymentDto
            {
                FullAmount = cashStatement.Payment.FullAmount,
                CashAmount = cashStatement.Payment.CashAmount,
                CashAmountsByCurrency = cashStatement.Payment.CashAmountsByCurrency
                    .Select(c => new CashAmountByCurrencyDto
                    {
                        CurrencyCode = c.CurrencyCode,
                        Amount = c.Amount
                    }).ToList(),
                PaymentTypes = cashStatement.Payment.PaymentTypes.Select(MapPaymentType).ToList()
            }
        };
    }

    private static string MapProcessType(ProcessType type)
    {
        return type switch
        {
            ProcessType.Receipt => "Beleg",
            ProcessType.Transfer => "AVTransfer",
            ProcessType.Order => "AVBestellung",
            ProcessType.Cancellation => "AVBelegstorno",
            ProcessType.Training => "AVTraining",
            ProcessType.BenefitInKind => "AVSachbezug",
            ProcessType.Invoice => "AVRechnung",
            ProcessType.Other => "AVSonstige",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    private static string MapBusinessTransactionType(BusinessTransactionType type)
    {
        return type switch
        {
            BusinessTransactionType.Umsatz => "Umsatz",
            BusinessTransactionType.Pfand => "Pfand",
            BusinessTransactionType.PfandRueckzahlung => "PfandRueckzahlung",
            BusinessTransactionType.Rabatt => "Rabatt",
            BusinessTransactionType.Aufschlag => "Aufschlag",
            BusinessTransactionType.MehrzweckgutscheinKauf => "MehrzweckgutscheinKauf",
            BusinessTransactionType.MehrzweckgutscheinEinloesung => "MehrzweckgutscheinEinloesung",
            BusinessTransactionType.EinzweckgutscheinKauf => "EinzweckgutscheinKauf",
            BusinessTransactionType.EinzweckgutscheinEinloesung => "EinzweckgutscheinEinloesung",
            BusinessTransactionType.Anzahlungseinstellung => "Anzahlungseinstellung",
            BusinessTransactionType.Anzahlungsaufloesung => "Anzahlungsaufloesung",
            BusinessTransactionType.Forderungsentstehung => "Forderungsentstehung",
            BusinessTransactionType.Forderungsaufloesung => "Forderungsaufloesung",
            BusinessTransactionType.Geldtransit => "Geldtransit",
            BusinessTransactionType.Einzahlung => "Einzahlung",
            BusinessTransactionType.Auszahlung => "Auszahlung",
            BusinessTransactionType.TrinkgeldAG => "TrinkgeldAG",
            BusinessTransactionType.TrinkgeldAN => "TrinkgeldAN",
            BusinessTransactionType.DifferenzSollIst => "DifferenzSollIst",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    private static CashPointClosingState MapState(CashPointClosingStateDto state)
    {
        return state switch
        {
            CashPointClosingStateDto.PENDING => CashPointClosingState.Pending,
            CashPointClosingStateDto.WORKING => CashPointClosingState.Working,
            CashPointClosingStateDto.COMPLETED => CashPointClosingState.Completed,
            CashPointClosingStateDto.ERROR => CashPointClosingState.Error,
            CashPointClosingStateDto.DELETED => CashPointClosingState.Deleted,
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
        };
    }
}
