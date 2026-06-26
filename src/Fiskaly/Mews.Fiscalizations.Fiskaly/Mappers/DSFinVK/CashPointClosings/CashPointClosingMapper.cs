using System.Globalization;
using Mews.Fiscalizations.Fiskaly.Models.DSFinVK.CashPointClosings;
using AmountPerVatDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.AmountPerVat;
using BusinessCaseDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.BusinessCase;
using BusinessCaseSummaryDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.BusinessCaseSummary;
using BuyerAddressDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.BuyerAddress;
using CashAmountByCurrencyDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.CashAmountByCurrency;
using CashPointClosingHeadDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.CashPointClosingHead;
using CashPointClosingResponseDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.CashPointClosingResponse;
using CashPointClosingStateDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.CashPointClosingState;
using CashPointClosingTransactionDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.CashPointClosingTransaction;
using CashStatementDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.CashStatement;
using CashStatementPaymentDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.CashStatementPayment;
using InsertCashPointClosingRequestDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.InsertCashPointClosingRequest;
using PaymentTypeAmountDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.PaymentTypeAmount;
using TransactionBuyerDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.TransactionBuyer;
using TransactionDataDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.TransactionData;
using TransactionHeadDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.TransactionHead;
using TransactionLineDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.TransactionLine;
using TransactionLineItemDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.TransactionLineItem;
using TransactionReferenceDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.TransactionReference;
using TransactionSecurityDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.TransactionSecurity;
using TransactionSubItemDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.TransactionSubItem;
using TransactionUserDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.TransactionUser;
using VatAmountBreakdownDto = Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings.VatAmountBreakdown;

namespace Mews.Fiscalizations.Fiskaly.Mappers.DSFinVK.CashPointClosings;

internal static class CashPointClosingMapper
{
    public static InsertCashPointClosingRequestDto MapInsertRequest(CashPointClosing closing)
    {
        var transactions = closing.Transactions?.ToList() ?? new List<CashPointClosingTransaction>();
        var hasTransactions = transactions.Count > 0;

        return new InsertCashPointClosingRequestDto
        {
            ClientId = closing.ClientId,
            CashPointClosingExportId = closing.CashPointClosingExportId,
            Head = new CashPointClosingHeadDto
            {
                ExportCreationDate = closing.ExportCreationDate.ToUnixTimeSeconds(),
                BusinessDate = closing.BusinessDate.ToString(
                    "yyyy-MM-dd",
                    CultureInfo.InvariantCulture
                ),
                FirstTransactionExportId = hasTransactions ? closing.FirstTransactionExportId : null,
                LastTransactionExportId = hasTransactions ? closing.LastTransactionExportId : null,
            },
            Transactions = hasTransactions ? transactions.Select(MapTransaction).ToList() : null,
            CashStatement = hasTransactions ? MapCashStatement(closing.CashStatement) : null,
        };
    }

    public static CashPointClosingResult MapResponse(this CashPointClosingResponseDto response)
    {
        return new CashPointClosingResult(
            Id: response.Id,
            ClientId: response.ClientId,
            CashPointClosingExportId: response.CashPointClosingExportId,
            State: MapState(response.State),
            Error: response.Error?.Message ?? response.Error?.Code
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
                Name = tx.Name,
                User = MapUser(tx.User),
                Buyer = MapBuyer(tx.Buyer),
                ClosingClientId = tx.ClosingClientId,
                References = tx.References?.Select(MapReference).ToList(),
                AllocationGroups = tx.AllocationGroups?.ToList(),
            },
            Data = new TransactionDataDto
            {
                FullAmountInclVat = tx.FullAmountInclVat,
                Lines = tx.Lines?.Select(MapLine).ToList(),
                AmountsPerVatId = tx.AmountsPerVat.Select(MapAmountPerVat).ToList(),
                PaymentTypes = tx.PaymentTypes.Select(MapPaymentType).ToList(),
                Notes = tx.Notes,
            },
            Security = new TransactionSecurityDto
            {
                TssTxId = tx.Security.TssTxId,
                ErrorMessage = tx.Security.ErrorMessage,
            },
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
            Date = reference.Date?.ToUnixTimeSeconds(),
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
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null),
        };
    }

    private static TransactionUserDto MapUser(TransactionUser user)
    {
        if (user == null)
        {
            return null;
        }
        return new TransactionUserDto { UserExportId = user.UserExportId, Name = user.Name };
    }

    private static TransactionBuyerDto MapBuyer(TransactionBuyer buyer)
    {
        if (buyer == null)
        {
            return null;
        }
        return new TransactionBuyerDto
        {
            Name = buyer.Name,
            BuyerExportId = buyer.BuyerExportId,
            Type = MapBuyerType(buyer.Type),
            VatIdNumber = buyer.VatIdNumber,
            Address = MapAddress(buyer.Address),
        };
    }

    private static string MapBuyerType(BuyerType type)
    {
        return type switch
        {
            BuyerType.Customer => "Kunde",
            BuyerType.Employee => "Mitarbeiter",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null),
        };
    }

    private static BuyerAddressDto MapAddress(BuyerAddress address)
    {
        if (address == null)
        {
            return null;
        }
        return new BuyerAddressDto
        {
            Street = address.Street,
            PostalCode = address.PostalCode,
            City = address.City,
            CountryCode = address.CountryCode,
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
                Name = line.BusinessCaseName,
                PurchaserAgencyId = line.PurchaserAgencyId,
                AmountsPerVatId = line.BusinessCaseAmountsPerVat.Select(MapAmountPerVat).ToList(),
            },
            InHouse = line.InHouse,
            References = line.References?.Select(MapReference).ToList(),
            VoucherId = line.VoucherId,
            ItemText = line.ItemText,
            Item =
                line.Item == null
                    ? null
                    : new TransactionLineItemDto
                    {
                        Number = line.Item.Number,
                        Gtin = line.Item.Gtin,
                        Quantity = line.Item.Quantity,
                        QuantityFactor = line.Item.QuantityFactor,
                        QuantityMeasure = line.Item.QuantityMeasure,
                        GroupId = line.Item.GroupId,
                        GroupName = line.Item.GroupName,
                        PricePerUnit = line.Item.PricePerUnit,
                        BaseAmountsPerVatId = line
                            .Item.BaseAmountsPerVat?.Select(MapVatBreakdown)
                            .ToList(),
                        DiscountsPerVatId = line
                            .Item.DiscountsPerVat?.Select(MapVatBreakdown)
                            .ToList(),
                        ExtraAmountsPerVatId = line
                            .Item.ExtraAmountsPerVat?.Select(MapVatBreakdown)
                            .ToList(),
                        SubItems = line.Item.SubItems?.Select(MapSubItem).ToList(),
                    },
            SourceCashRegister = line.SourceCashRegister,
        };
    }

    private static AmountPerVatDto MapAmountPerVat(AmountPerVat amount)
    {
        return new AmountPerVatDto
        {
            VatDefinitionExportId = amount.VatDefinitionExportId,
            GrossAmount = amount.GrossAmount,
            NetAmount = amount.NetAmount,
            TaxAmount = amount.TaxAmount,
        };
    }

    private static VatAmountBreakdownDto MapVatBreakdown(VatAmountBreakdown amount)
    {
        return new VatAmountBreakdownDto
        {
            VatDefinitionExportId = amount.VatDefinitionExportId,
            InclVat = amount.InclVat,
            ExclVat = amount.ExclVat,
            Vat = amount.Vat,
        };
    }

    private static TransactionSubItemDto MapSubItem(TransactionSubItem subItem)
    {
        return new TransactionSubItemDto
        {
            Number = subItem.Number,
            Gtin = subItem.Gtin,
            Name = subItem.Name,
            Quantity = subItem.Quantity,
            QuantityFactor = subItem.QuantityFactor,
            QuantityMeasure = subItem.QuantityMeasure,
            GroupId = subItem.GroupId,
            GroupName = subItem.GroupName,
            AmountPerVatId =
                subItem.AmountPerVat == null ? null : MapVatBreakdown(subItem.AmountPerVat),
        };
    }

    private static PaymentTypeAmountDto MapPaymentType(PaymentTypeAmount payment)
    {
        return new PaymentTypeAmountDto
        {
            PaymentType = MapPaymentTypeName(payment.PaymentType),
            Name = payment.Name,
            Amount = payment.Amount,
            ForeignAmount = payment.ForeignAmount,
            CurrencyCode = payment.CurrencyCode,
        };
    }

    private static string MapPaymentTypeName(PaymentType type)
    {
        return type switch
        {
            PaymentType.Cash => "Bar",
            PaymentType.NonCash => "Unbar",
            PaymentType.DebitCard => "ECKarte",
            PaymentType.CreditCard => "Kreditkarte",
            PaymentType.ElectronicPaymentServiceProvider => "ElZahlungsdienstleister",
            PaymentType.VoucherCard => "Guthabenkarte",
            PaymentType.None => "Keine",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null),
        };
    }

    private static CashStatementDto MapCashStatement(CashStatement cashStatement)
    {
        return new CashStatementDto
        {
            BusinessCases = cashStatement
                .BusinessCases.Select(bc => new BusinessCaseSummaryDto
                {
                    Type = MapBusinessTransactionType(bc.Type),
                    AmountsPerVatId = bc.AmountsPerVat.Select(MapAmountPerVat).ToList(),
                })
                .ToList(),
            Payment = new CashStatementPaymentDto
            {
                FullAmount = cashStatement.Payment.FullAmount,
                CashAmount = cashStatement.Payment.CashAmount,
                CashAmountsByCurrency = cashStatement
                    .Payment.CashAmountsByCurrency.Select(c => new CashAmountByCurrencyDto
                    {
                        CurrencyCode = c.CurrencyCode,
                        Amount = c.Amount,
                    })
                    .ToList(),
                PaymentTypes = cashStatement.Payment.PaymentTypes.Select(MapPaymentType).ToList(),
            },
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
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null),
        };
    }

    private static string MapBusinessTransactionType(BusinessTransactionType type)
    {
        return type switch
        {
            BusinessTransactionType.Anfangsbestand => "Anfangsbestand",
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
            BusinessTransactionType.Privateinlage => "Privateinlage",
            BusinessTransactionType.Privatentnahme => "Privatentnahme",
            BusinessTransactionType.Geldtransit => "Geldtransit",
            BusinessTransactionType.Einzahlung => "Einzahlung",
            BusinessTransactionType.Auszahlung => "Auszahlung",
            BusinessTransactionType.Lohnzahlung => "Lohnzahlung",
            BusinessTransactionType.TrinkgeldAG => "TrinkgeldAG",
            BusinessTransactionType.TrinkgeldAN => "TrinkgeldAN",
            BusinessTransactionType.ZuschussEcht => "ZuschussEcht",
            BusinessTransactionType.ZuschussUnecht => "ZuschussUnecht",
            BusinessTransactionType.DifferenzSollIst => "DifferenzSollIst",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null),
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
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, null),
        };
    }
}
