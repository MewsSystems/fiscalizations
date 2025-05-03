using System.Globalization;
using Mews.Fiscalizations.Fiskaly.DTOs.SignES.Invoice;
using Mews.Fiscalizations.Fiskaly.DTOs.SignES.Taxpayer;
using Mews.Fiscalizations.Fiskaly.Models.SignES.Invoice;
using Mews.Fiscalizations.Fiskaly.Models.SignES.Taxpayer;
using TaxExemptionReason = Mews.Fiscalizations.Fiskaly.Models.SignES.Invoice.TaxExemptionReason;

namespace Mews.Fiscalizations.Fiskaly.Mappers.SignES;

internal static class RequestMapper
{
    public static CreateTaxpayerRequest CreateTaxpayer(string legalName, string taxIdentifier, TaxpayerTerritory territory)
    {
        return new CreateTaxpayerRequest
        {
            Data = new TaxpayerDataRequest
            {
                Issuer = new TaxpayerIssuer
                {
                    LegalName = legalName,
                    TaxNumber = taxIdentifier
                },
                Territory = territory.ToDto()
            }
        };
    }

    public static SimplifiedInvoiceRequest CreateSimplifiedInvoice(SimplifiedInvoice simplifiedInvoice)
    {
        return new SimplifiedInvoiceRequest
        {
            Content = new SimplifiedInvoiceData
            {
                Text = simplifiedInvoice.InvoiceDescription,
                Number = simplifiedInvoice.InvoiceNumber,
                FullAmount = simplifiedInvoice.FullAmount.ToString("F2", CultureInfo.InvariantCulture),
                Items = simplifiedInvoice.Items.Select(i => CreateLineItem(i)).ToList()
            }
        };
    }

    public static CompleteInvoiceRequest CreateCompleteInvoice(CompleteInvoice completeInvoice)
    {
        return new CompleteInvoiceRequest
        {
            Content = new CompleteInvoiceData
            {
                Data = CreateSimplifiedInvoice(completeInvoice.simplifiedInvoice).Content,
                Recipients = completeInvoice.Receivers.Select(r => r.ToDto()).ToList()
            }
        };
    }

    private static Item CreateLineItem(InvoiceItem lineItem)
    {
        var category = lineItem.TaxExemptionReason == Models.SignES.Invoice.TaxExemptionReason.NotExempt ? 
            new Category
            {
                Type = TaxCategoryType.VAT,
                Rate = lineItem.TaxRate?.ToString("F2", CultureInfo.InvariantCulture)
            }:
            new Category
            {
                Type = TaxCategoryType.NO_VAT,
                Cause = lineItem.TaxExemptionReason.ToDto()
            };

        return new Item
        {
            Text = lineItem.ItemDescription,
            Quantity = lineItem.Quantity.ToString("F2", CultureInfo.InvariantCulture),
            UnitAmount = lineItem.UnitAmount.ToString("F2", CultureInfo.InvariantCulture),
            FullAmount = lineItem.FullAmount.ToString("F2", CultureInfo.InvariantCulture),
            System = new DTOs.SignES.Invoice.System
            {
                Category = category
            }
        };
    }

    private static DTOs.SignES.Invoice.TaxExemptionReason ToDto(this TaxExemptionReason reason)
    {
        return reason switch
        {
            Models.SignES.Invoice.TaxExemptionReason.Article20 => DTOs.SignES.Invoice.TaxExemptionReason.TAXABLE_EXEMPT_1,
            Models.SignES.Invoice.TaxExemptionReason.Article21 => DTOs.SignES.Invoice.TaxExemptionReason.TAXABLE_EXEMPT_2,
            Models.SignES.Invoice.TaxExemptionReason.Article22 => DTOs.SignES.Invoice.TaxExemptionReason.TAXABLE_EXEMPT_3,
            Models.SignES.Invoice.TaxExemptionReason.Article24 => DTOs.SignES.Invoice.TaxExemptionReason.TAXABLE_EXEMPT_4,
            Models.SignES.Invoice.TaxExemptionReason.Article25 => DTOs.SignES.Invoice.TaxExemptionReason.TAXABLE_EXEMPT_5,
            Models.SignES.Invoice.TaxExemptionReason.OtherGrounds => DTOs.SignES.Invoice.TaxExemptionReason.TAXABLE_EXEMPT_6,
            _ => throw new ArgumentOutOfRangeException(nameof(reason), reason, null)
        };
    }

    private static Territory ToDto(this TaxpayerTerritory territory)
    {
        return territory switch
        {
            TaxpayerTerritory.Araba => Territory.ARABA,
            TaxpayerTerritory.Bizkaia => Territory.BIZKAIA,
            TaxpayerTerritory.Gipuzkoa => Territory.GIPUZKOA,
            TaxpayerTerritory.Navarre => Territory.NAVARRE,
            TaxpayerTerritory.CanaryIslands => Territory.CANARY_ISLANDS,
            TaxpayerTerritory.Ceuta => Territory.CEUTA,
            TaxpayerTerritory.Melilla => Territory.MELILLA,
            TaxpayerTerritory.SpainOther => Territory.SPAIN_OTHER,
            _ => throw new ArgumentOutOfRangeException(nameof(territory), territory, null)
        };
    }

    private static RecipientRequest ToDto(this Receiver receiver)
    {
        return new RecipientRequest
        {
            Id = receiver.Type == ReceiverType.Local ? receiver.ToLocalDto() : receiver.ToForeignDto(),
            AddressLine = receiver.Address,
            PostalCode = receiver.PostalCode
        };
    }

    private static NationalIdentification ToLocalDto(this Receiver receiver)
    {
        return new NationalIdentification
        {
            LegalName = receiver.LegalName,
            TaxNumber = receiver.TaxIdentifier
        };
    }

    private static InternationalIdentification ToForeignDto(this Receiver receiver)
    {
        return new InternationalIdentification
        {
            LegalName = receiver.LegalName,
            Type = receiver.ForeignerDocumentType.ToDto(),
            Number = receiver.TaxIdentifier,
            CountryCode = receiver.DocumentCountry
        };
    }

    private static IdentificationType ToDto(this ForeignerDocumentType type)
    {
        return type switch
        {
            ForeignerDocumentType.TaxIdentifier => IdentificationType.TAX_NUMBER,
            ForeignerDocumentType.Passport => IdentificationType.PASSPORT,
            ForeignerDocumentType.OfficialId => IdentificationType.DOCUMENT,
            ForeignerDocumentType.ResidenceId => IdentificationType.CERTIFICATE,
            ForeignerDocumentType.Other => IdentificationType.OTHER,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}