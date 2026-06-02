using System.Globalization;
using Mews.Fiscalizations.Fiskaly.Models.DSFinVK.CashPointClosings;
using AmountPerVatDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.AmountPerVat;
using AllocationGroupDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.AllocationGroup;
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
                FirstTransactionExportId = transactions.FirstOrDefault()?.TransactionExportId,
                LastTransactionExportId = transactions.LastOrDefault()?.TransactionExportId
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
                References = tx.References?.Select(MapReference).ToList()
            },
            Data = new TransactionDataDto
            {
                FullAmountInclVat = tx.FullAmountInclVat.ToString("F2", CultureInfo.InvariantCulture),
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
            Type = "InterneTransaktion",
            TxId = reference.TxId,
            ClosingId = reference.ClosingId
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
            Quantity = line.Quantity.ToString("F2", CultureInfo.InvariantCulture),
            PricePerUnit = line.PricePerUnit.ToString("F2", CultureInfo.InvariantCulture),
            GrossAmount = line.GrossAmount.ToString("F2", CultureInfo.InvariantCulture),
            NetAmount = line.NetAmount.ToString("F2", CultureInfo.InvariantCulture)
        };
    }

    private static AmountPerVatDto MapAmountPerVat(AmountPerVat amount)
    {
        return new AmountPerVatDto
        {
            VatDefinitionExportId = amount.VatDefinitionExportId,
            GrossAmount = amount.GrossAmount.ToString("F2", CultureInfo.InvariantCulture),
            NetAmount = amount.NetAmount.ToString("F2", CultureInfo.InvariantCulture),
            TaxAmount = amount.TaxAmount.ToString("F2", CultureInfo.InvariantCulture)
        };
    }

    private static PaymentTypeAmountDto MapPaymentType(PaymentTypeAmount payment)
    {
        return new PaymentTypeAmountDto
        {
            PaymentType = payment.PaymentType,
            Amount = payment.Amount.ToString("F2", CultureInfo.InvariantCulture),
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
                FullAmount = cashStatement.Payment.FullAmount.ToString("F2", CultureInfo.InvariantCulture),
                CashAmount = cashStatement.Payment.CashAmount.ToString("F2", CultureInfo.InvariantCulture),
                CashAmountsByCurrency = cashStatement.Payment.CashAmountsByCurrency
                    .Select(c => new CashAmountByCurrencyDto
                    {
                        CurrencyCode = c.CurrencyCode,
                        Amount = c.Amount.ToString("F2", CultureInfo.InvariantCulture)
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
