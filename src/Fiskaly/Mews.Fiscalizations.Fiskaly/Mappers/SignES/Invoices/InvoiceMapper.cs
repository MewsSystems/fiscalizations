using System.Globalization;
using Mews.Fiscalizations.Fiskaly.DTOs.SignES;
using Mews.Fiscalizations.Fiskaly.DTOs.SignES.Invoices;
using Mews.Fiscalizations.Fiskaly.Models.SignES.Invoices;
using InvoiceResponse = Mews.Fiscalizations.Fiskaly.Models.SignES.Invoices.InvoiceResponse;
using InvoiceState = Mews.Fiscalizations.Fiskaly.Models.SignES.Invoices.InvoiceState;
using SignedInvoiceCancellationState = Mews.Fiscalizations.Fiskaly.Models.SignES.Invoices.SignedInvoiceCancellationState;
using SignedInvoiceRegistrationState = Mews.Fiscalizations.Fiskaly.Models.SignES.Invoices.SignedInvoiceRegistrationState;
using TaxExemptionReason = Mews.Fiscalizations.Fiskaly.Models.SignES.Invoices.TaxExemptionReason;

namespace Mews.Fiscalizations.Fiskaly.Mappers.SignES.Invoices;

internal static class InvoiceMapper
{
    public static ContentWrapper<SimplifiedInvoiceRequest> MapSimplifiedInvoiceRequest(SimplifiedInvoice simplifiedInvoice)
    {
        return new ContentWrapper<SimplifiedInvoiceRequest>
        {
            Content = new SimplifiedInvoiceRequest
            {
                Text = simplifiedInvoice.InvoiceDescription,
                Number = simplifiedInvoice.InvoiceNumber,
                FullAmount = simplifiedInvoice.FullAmount.ToString("F2", CultureInfo.InvariantCulture),
                Items = simplifiedInvoice.Items.Select(MapInvoiceItemRequest).ToList()
            }
        };
    }
    
    public static ContentWrapper<CompleteInvoiceRequest> MapCompleteInvoiceRequest(CompleteInvoice completeInvoice)
    {
        return new ContentWrapper<CompleteInvoiceRequest>
        {
            Content = new CompleteInvoiceRequest
            {
                Data = MapSimplifiedInvoiceRequest(completeInvoice.simplifiedInvoice).Content,
                Recipients = completeInvoice.Receivers.Select(r => r.MapReceiverRequest()).ToList()
            }
        };
    }
    
    public static InvoiceResponse MapInvoiceResponse(this DTOs.SignES.Invoices.InvoiceResponse response)
    {
        return new InvoiceResponse(
            InvoiceId: response.Content.Id,
            SignerId: response.Content.Signer.Id,
            ClientId: response.Content.Client.Id,
            InvoiceJsonData: response.Content.Data,
            ComplianceData: new InvoiceComplianceData(
                Base64ImageData: response.Content.Compliance.Code.Image.Data,
                ImageFormat: response.Content.Compliance.Code.Image.Format,
                VerifactuValidationUrl: response.Content.Compliance.Url,
                VerifactuInvoiceText: response.Content.Compliance.Text
            ),
            State: response.Content.State.MapInvoiceStateResponse(),
            Transmission: new SignedInvoiceTransmission(
                RegistrationState: response.Content.Transmission.Registration.MapInvoiceRegistrationStateResponse(),
                CancellationState: response.Content.Transmission.Cancellation.MapSignedInvoiceCancellationState()
            ),
            Validations: response.Content.Validations.Select(v => new InvoiceValidationData(
                ErrorCode: v.Code,
                Description: v.Description
            ))
        );
    }
    
    private static Item MapInvoiceItemRequest(InvoiceItem lineItem)
    {
        var category = lineItem.TaxExemptionReason == TaxExemptionReason.NotExempt ? 
            new Category
            {
                Type = TaxCategoryType.VAT,
                Rate = lineItem.TaxRate?.ToString("F2", CultureInfo.InvariantCulture)
            }:
            new Category
            {
                Type = TaxCategoryType.NO_VAT,
                Cause = lineItem.TaxExemptionReason.MapTaxExemptionReasonRequest()
            };

        return new Item
        {
            Text = lineItem.ItemDescription,
            Quantity = lineItem.Quantity.ToString("F2", CultureInfo.InvariantCulture),
            UnitAmount = lineItem.UnitAmount.ToString("F2", CultureInfo.InvariantCulture),
            FullAmount = lineItem.FullAmount.ToString("F2", CultureInfo.InvariantCulture),
            System = new DTOs.SignES.Invoices.System
            {
                Category = category
            }
        };
    }
    
    private static DTOs.SignES.Invoices.TaxExemptionReason MapTaxExemptionReasonRequest(this TaxExemptionReason reason)
    {
        return reason switch
        {
            TaxExemptionReason.Article20 => DTOs.SignES.Invoices.TaxExemptionReason.TAXABLE_EXEMPT_1,
            TaxExemptionReason.Article21 => DTOs.SignES.Invoices.TaxExemptionReason.TAXABLE_EXEMPT_2,
            TaxExemptionReason.Article22 => DTOs.SignES.Invoices.TaxExemptionReason.TAXABLE_EXEMPT_3,
            TaxExemptionReason.Article24 => DTOs.SignES.Invoices.TaxExemptionReason.TAXABLE_EXEMPT_4,
            TaxExemptionReason.Article25 => DTOs.SignES.Invoices.TaxExemptionReason.TAXABLE_EXEMPT_5,
            TaxExemptionReason.OtherGrounds => DTOs.SignES.Invoices.TaxExemptionReason.TAXABLE_EXEMPT_6,
            _ => throw new ArgumentOutOfRangeException(nameof(reason), reason, null)
        };
    }
    
    private static RecipientRequest MapReceiverRequest(this Receiver receiver)
    {
        return new RecipientRequest
        {
            Id = receiver.Type == ReceiverType.Local ? receiver.MapLocalReceiverRequest() : receiver.MapForeignReceiverRequest(),
            AddressLine = receiver.Address,
            PostalCode = receiver.PostalCode
        };
    }
    
    private static NationalIdentification MapLocalReceiverRequest(this Receiver receiver)
    {
        return new NationalIdentification
        {
            LegalName = receiver.LegalName,
            TaxNumber = receiver.TaxIdentifier
        };
    }

    private static InternationalIdentification MapForeignReceiverRequest(this Receiver receiver)
    {
        return new InternationalIdentification
        {
            LegalName = receiver.LegalName,
            Type = receiver.ForeignerDocumentType.MapForeignReceiverDocumentTypeRequest(),
            Number = receiver.TaxIdentifier,
            CountryCode = receiver.DocumentCountry
        };
    }
    
    private static IdentificationType MapForeignReceiverDocumentTypeRequest(this ForeignerDocumentType type)
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

    private static InvoiceState MapInvoiceStateResponse(this DTOs.SignES.Invoices.InvoiceState state)
    {
        return state switch
        {
            DTOs.SignES.Invoices.InvoiceState.ISSUED => InvoiceState.Issued,
            DTOs.SignES.Invoices.InvoiceState.CANCELLED => InvoiceState.Canceled,
            DTOs.SignES.Invoices.InvoiceState.IMPORTED => InvoiceState.Imported,
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
        };
    }

    private static SignedInvoiceRegistrationState MapInvoiceRegistrationStateResponse(this DTOs.SignES.Invoices.SignedInvoiceRegistrationState state)
    {
        return state switch
        {
            DTOs.SignES.Invoices.SignedInvoiceRegistrationState.PENDING => SignedInvoiceRegistrationState.Pending,
            DTOs.SignES.Invoices.SignedInvoiceRegistrationState.REGISTERED => SignedInvoiceRegistrationState.Registered,
            DTOs.SignES.Invoices.SignedInvoiceRegistrationState.REQUIRES_CORRECTION => SignedInvoiceRegistrationState.RequiresCorrection,
            DTOs.SignES.Invoices.SignedInvoiceRegistrationState.REQUIRES_INSPECTION => SignedInvoiceRegistrationState.RequiresInspection,
            DTOs.SignES.Invoices.SignedInvoiceRegistrationState.STORED => SignedInvoiceRegistrationState.Stored,
            DTOs.SignES.Invoices.SignedInvoiceRegistrationState.INVALID => SignedInvoiceRegistrationState.Invalid,
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
        };
    }

    private static SignedInvoiceCancellationState MapSignedInvoiceCancellationState(this DTOs.SignES.Invoices.SignedInvoiceCancellationState state)
    {
        return state switch
        {
            DTOs.SignES.Invoices.SignedInvoiceCancellationState.NOT_CANCELLED => SignedInvoiceCancellationState.NotCancelled,
            DTOs.SignES.Invoices.SignedInvoiceCancellationState.PENDING => SignedInvoiceCancellationState.Pending,
            DTOs.SignES.Invoices.SignedInvoiceCancellationState.CANCELLED => SignedInvoiceCancellationState.Cancelled,
            DTOs.SignES.Invoices.SignedInvoiceCancellationState.REQUIRES_INSPECTION => SignedInvoiceCancellationState.RequiresInspection,
            DTOs.SignES.Invoices.SignedInvoiceCancellationState.STORED => SignedInvoiceCancellationState.Stored,
            DTOs.SignES.Invoices.SignedInvoiceCancellationState.INVALID => SignedInvoiceCancellationState.Invalid,
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
        };
    }
}