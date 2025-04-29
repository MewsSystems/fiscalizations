using System.Globalization;
using Mews.Fiscalizations.Verifactu.Models;

namespace Mews.Fiscalizations.Verifactu;

internal static class RequestMapper
{
    public static DTOs.AuthorizationTokenRequest CreateAuthorizationToken(string apiKey, string apiSecret)
    {
        return new DTOs.AuthorizationTokenRequest
        {
            Content = new DTOs.AuthorizationTokenRequestContent
            {
                ApiKey = apiKey,
                ApiSecret = apiSecret
            }
        };
    }

    public static DTOs.CreateTaxpayerRequest CreateTaxpayer(string legalName, string taxIdentifier, TaxpayerTerritory territory)
    {
        return new DTOs.CreateTaxpayerRequest
        {
            Data = new DTOs.TaxpayerDataRequest
            {
                Issuer = new DTOs.TaxpayerIssuer
                {
                    LegalName = legalName,
                    TaxNumber = taxIdentifier
                },
                Territory = territory.ToDto()
            }
        };
    }

    public static DTOs.SimplifiedInvoiceRequest CreateSimplifiedInvoice(SimplifiedInvoice simplifiedInvoice)
    {
        return new DTOs.SimplifiedInvoiceRequest
        {
            Content = new DTOs.SimplifiedInvoiceData
            {
                Text = simplifiedInvoice.InvoiceDescription,
                Number = simplifiedInvoice.InvoiceNumber,
                FullAmount = simplifiedInvoice.FullAmount.ToString("F2", CultureInfo.InvariantCulture),
                Items = simplifiedInvoice.Items.Select(i => CreateLineItem(i)).ToList()
            }
        };
    }

    public static DTOs.CompleteInvoiceRequest CreateCompleteInvoice(CompleteInvoice completeInvoice)
    {
        return new DTOs.CompleteInvoiceRequest
        {
            Content = new DTOs.CompleteInvoiceData
            {
                Data = CreateSimplifiedInvoice(completeInvoice.simplifiedInvoice).Content,
                Recipients = completeInvoice.Receivers.Select(r => r.ToDto()).ToList()
            }
        };
    }

    private static DTOs.Item CreateLineItem(InvoiceItem lineItem)
    {
        var category = lineItem.TaxExemptionReason == TaxExemptionReason.NotExempt ? 
            new DTOs.Category
            {
                Type = DTOs.TaxCategoryType.VAT,
                Rate = lineItem.TaxRate?.ToString("F2", CultureInfo.InvariantCulture)
            }:
            new DTOs.Category
            {
                Type = DTOs.TaxCategoryType.NO_VAT,
                Cause = lineItem.TaxExemptionReason.ToDto()
            };

        return new DTOs.Item
        {
            Text = lineItem.ItemDescription,
            Quantity = lineItem.Quantity.ToString("F2", CultureInfo.InvariantCulture),
            UnitAmount = lineItem.UnitAmount.ToString("F2", CultureInfo.InvariantCulture),
            FullAmount = lineItem.FullAmount.ToString("F2", CultureInfo.InvariantCulture),
            System = new DTOs.System
            {
                Category = category
            }
        };
    }

    private static DTOs.TaxExemptionReason ToDto(this TaxExemptionReason reason)
    {
        return reason switch
        {
            TaxExemptionReason.Article20 => DTOs.TaxExemptionReason.TAXABLE_EXEMPT_1,
            TaxExemptionReason.Article21 => DTOs.TaxExemptionReason.TAXABLE_EXEMPT_2,
            TaxExemptionReason.Article22 => DTOs.TaxExemptionReason.TAXABLE_EXEMPT_3,
            TaxExemptionReason.Article24 => DTOs.TaxExemptionReason.TAXABLE_EXEMPT_4,
            TaxExemptionReason.Article25 => DTOs.TaxExemptionReason.TAXABLE_EXEMPT_5,
            TaxExemptionReason.OtherGrounds => DTOs.TaxExemptionReason.TAXABLE_EXEMPT_6,
            _ => throw new ArgumentOutOfRangeException(nameof(reason), reason, null)
        };
    }

    private static DTOs.Territory ToDto(this TaxpayerTerritory territory)
    {
        return territory switch
        {
            TaxpayerTerritory.Araba => DTOs.Territory.ARABA,
            TaxpayerTerritory.Bizkaia => DTOs.Territory.BIZKAIA,
            TaxpayerTerritory.Gipuzkoa => DTOs.Territory.GIPUZKOA,
            TaxpayerTerritory.Navarre => DTOs.Territory.NAVARRE,
            TaxpayerTerritory.CanaryIslands => DTOs.Territory.CANARY_ISLANDS,
            TaxpayerTerritory.Ceuta => DTOs.Territory.CEUTA,
            TaxpayerTerritory.Melilla => DTOs.Territory.MELILLA,
            TaxpayerTerritory.SpainOther => DTOs.Territory.SPAIN_OTHER,
            _ => throw new ArgumentOutOfRangeException(nameof(territory), territory, null)
        };
    }

    private static DTOs.RecipientRequest ToDto(this Receiver receiver)
    {
        return new DTOs.RecipientRequest
        {
            Id = receiver.Match<object>(
                localReceiver => localReceiver.ToDto(),
                foreignReceiver => foreignReceiver.ToDto()
            ),
            AddressLine = receiver.Address,
            PostalCode = receiver.PostalCode
        };
    }

    private static DTOs.NationalIdentification ToDto(this LocalReceiver receiver)
    {
        return new DTOs.NationalIdentification
        {
            LegalName = receiver.Name,
            TaxNumber = receiver.TaxIdentifier
        };
    }

    private static DTOs.InternationalIdentification ToDto(this ForeignReceiver receiver)
    {
        return new DTOs.InternationalIdentification
        {
            LegalName = receiver.Name,
            Type = receiver.DocumentType.ToDto(),
            Number = receiver.DocumentNumber,
            CountryCode = receiver.DocumentCountry.Alpha2Code
        };
    }

    private static DTOs.IdentificationType ToDto(this ForeignerDocumentType type)
    {
        return type switch
        {
            ForeignerDocumentType.TaxIdentifier => DTOs.IdentificationType.TAX_NUMBER,
            ForeignerDocumentType.Passport => DTOs.IdentificationType.PASSPORT,
            ForeignerDocumentType.OfficialId => DTOs.IdentificationType.DOCUMENT,
            ForeignerDocumentType.ResidenceId => DTOs.IdentificationType.CERTIFICATE,
            ForeignerDocumentType.Other => DTOs.IdentificationType.OTHER,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}