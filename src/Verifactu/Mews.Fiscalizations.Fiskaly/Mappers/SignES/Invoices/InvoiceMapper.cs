using System.Globalization;
using Mews.Fiscalizations.Fiskaly.DTOs.SignES.Invoice;
using Mews.Fiscalizations.Fiskaly.Models.SignES.Invoice;
using InvoiceErrorCode = Mews.Fiscalizations.Fiskaly.Models.SignES.Invoice.InvoiceErrorCode;
using InvoiceResponse = Mews.Fiscalizations.Fiskaly.Models.SignES.Invoice.InvoiceResponse;
using InvoiceState = Mews.Fiscalizations.Fiskaly.Models.SignES.Invoice.InvoiceState;
using SignedInvoiceCancellationState = Mews.Fiscalizations.Fiskaly.Models.SignES.Invoice.SignedInvoiceCancellationState;
using SignedInvoiceRegistrationState = Mews.Fiscalizations.Fiskaly.Models.SignES.Invoice.SignedInvoiceRegistrationState;
using TaxExemptionReason = Mews.Fiscalizations.Fiskaly.Models.SignES.Invoice.TaxExemptionReason;

namespace Mews.Fiscalizations.Fiskaly.Mappers.SignES.Invoices;

internal static class InvoiceMapper
{
    public static SimplifiedInvoiceRequest MapSimplifiedInvoiceRequest(SimplifiedInvoice simplifiedInvoice)
    {
        return new SimplifiedInvoiceRequest
        {
            Content = new SimplifiedInvoiceData
            {
                Text = simplifiedInvoice.InvoiceDescription,
                Number = simplifiedInvoice.InvoiceNumber,
                FullAmount = simplifiedInvoice.FullAmount.ToString("F2", CultureInfo.InvariantCulture),
                Items = simplifiedInvoice.Items.Select(MapInvoiceItemRequest).ToList()
            }
        };
    }
    
    public static CompleteInvoiceRequest MapCompleteInvoiceRequest(CompleteInvoice completeInvoice)
    {
        return new CompleteInvoiceRequest
        {
            Content = new CompleteInvoiceData
            {
                Data = MapSimplifiedInvoiceRequest(completeInvoice.simplifiedInvoice).Content,
                Recipients = completeInvoice.Receivers.Select(r => r.MapReceiverRequest()).ToList()
            }
        };
    }
    
    public static InvoiceResponse MapInvoiceResponse(this DTOs.SignES.Invoice.InvoiceResponse response)
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
                ErrorCode: v.Code.MapInvoiceErrorCode(),
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
            System = new DTOs.SignES.Invoice.System
            {
                Category = category
            }
        };
    }
    
    private static DTOs.SignES.Invoice.TaxExemptionReason MapTaxExemptionReasonRequest(this TaxExemptionReason reason)
    {
        return reason switch
        {
            TaxExemptionReason.Article20 => DTOs.SignES.Invoice.TaxExemptionReason.TAXABLE_EXEMPT_1,
            TaxExemptionReason.Article21 => DTOs.SignES.Invoice.TaxExemptionReason.TAXABLE_EXEMPT_2,
            TaxExemptionReason.Article22 => DTOs.SignES.Invoice.TaxExemptionReason.TAXABLE_EXEMPT_3,
            TaxExemptionReason.Article24 => DTOs.SignES.Invoice.TaxExemptionReason.TAXABLE_EXEMPT_4,
            TaxExemptionReason.Article25 => DTOs.SignES.Invoice.TaxExemptionReason.TAXABLE_EXEMPT_5,
            TaxExemptionReason.OtherGrounds => DTOs.SignES.Invoice.TaxExemptionReason.TAXABLE_EXEMPT_6,
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

    private static InvoiceState MapInvoiceStateResponse(this DTOs.SignES.Invoice.InvoiceState state)
    {
        return state switch
        {
            DTOs.SignES.Invoice.InvoiceState.ISSUED => InvoiceState.Issued,
            DTOs.SignES.Invoice.InvoiceState.CANCELLED => InvoiceState.Canceled,
            DTOs.SignES.Invoice.InvoiceState.IMPORTED => InvoiceState.Imported,
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
        };
    }

    private static SignedInvoiceRegistrationState MapInvoiceRegistrationStateResponse(this DTOs.SignES.Invoice.SignedInvoiceRegistrationState state)
    {
        return state switch
        {
            DTOs.SignES.Invoice.SignedInvoiceRegistrationState.PENDING => SignedInvoiceRegistrationState.Pending,
            DTOs.SignES.Invoice.SignedInvoiceRegistrationState.REGISTERED => SignedInvoiceRegistrationState.Registered,
            DTOs.SignES.Invoice.SignedInvoiceRegistrationState.REQUIRES_CORRECTION => SignedInvoiceRegistrationState.RequiresCorrection,
            DTOs.SignES.Invoice.SignedInvoiceRegistrationState.REQUIRES_INSPECTION => SignedInvoiceRegistrationState.RequiresInspection,
            DTOs.SignES.Invoice.SignedInvoiceRegistrationState.STORED => SignedInvoiceRegistrationState.Stored,
            DTOs.SignES.Invoice.SignedInvoiceRegistrationState.INVALID => SignedInvoiceRegistrationState.Invalid,
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
        };
    }

    private static SignedInvoiceCancellationState MapSignedInvoiceCancellationState(this DTOs.SignES.Invoice.SignedInvoiceCancellationState state)
    {
        return state switch
        {
            DTOs.SignES.Invoice.SignedInvoiceCancellationState.NOT_CANCELLED => SignedInvoiceCancellationState.NotCancelled,
            DTOs.SignES.Invoice.SignedInvoiceCancellationState.PENDING => SignedInvoiceCancellationState.Pending,
            DTOs.SignES.Invoice.SignedInvoiceCancellationState.CANCELLED => SignedInvoiceCancellationState.Cancelled,
            DTOs.SignES.Invoice.SignedInvoiceCancellationState.REQUIRES_INSPECTION => SignedInvoiceCancellationState.RequiresInspection,
            DTOs.SignES.Invoice.SignedInvoiceCancellationState.STORED => SignedInvoiceCancellationState.Stored,
            DTOs.SignES.Invoice.SignedInvoiceCancellationState.INVALID => SignedInvoiceCancellationState.Invalid,
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
        };
    }

    private static InvoiceErrorCode MapInvoiceErrorCode(this DTOs.SignES.Invoice.InvoiceErrorCode code)
    {
        return code switch
        {
            DTOs.SignES.Invoice.InvoiceErrorCode.V_INCORRECT_SENDER_CERTIFICATE => InvoiceErrorCode.IncorrectSenderCertificate,
            DTOs.SignES.Invoice.InvoiceErrorCode.V_XSD_SCHEMA_NOT_COMPLY => InvoiceErrorCode.XsdSchemaNotComply,
            DTOs.SignES.Invoice.InvoiceErrorCode.V_INVOICE_WITHOUT_LINES => InvoiceErrorCode.InvoiceWithoutLines,
            DTOs.SignES.Invoice.InvoiceErrorCode.V_REQUIRED_FIELD_MISSING_OR_INCORRECT => InvoiceErrorCode.RequiredFieldIncorrectOrMissing,
            DTOs.SignES.Invoice.InvoiceErrorCode.V_INVOICE_ALREADY_REGISTERED => InvoiceErrorCode.InvoiceAlreadyRegistered,
            DTOs.SignES.Invoice.InvoiceErrorCode.V_INVOICE_ALREADY_CANCELLED => InvoiceErrorCode.InvoiceAlreadyCancelled,
            DTOs.SignES.Invoice.InvoiceErrorCode.V_SERVICE_NOT_AVAILABLE => InvoiceErrorCode.ServiceNotAvailable,
            DTOs.SignES.Invoice.InvoiceErrorCode.V_INVALID_SENDER_CERTIFICATE => InvoiceErrorCode.InvalidSenderCertificate,
            DTOs.SignES.Invoice.InvoiceErrorCode.V_WRONG_SIGNATURE => InvoiceErrorCode.WrongSignature,
            DTOs.SignES.Invoice.InvoiceErrorCode.V_INCORRECT_INVOICE_CHAINING => InvoiceErrorCode.IncorrectInvoiceChaining,
            DTOs.SignES.Invoice.InvoiceErrorCode.V_BUSINESS_VALIDATION_DATA_ERROR => InvoiceErrorCode.BusinessValidationError,
            DTOs.SignES.Invoice.InvoiceErrorCode.V_DEVICE_NOT_REGISTERED => InvoiceErrorCode.DeviceNotRegistered,
            DTOs.SignES.Invoice.InvoiceErrorCode.V_EXPIRED_SIGNATURE_CERTIFICATE => InvoiceErrorCode.ExpiredSignatureCertificate,
            DTOs.SignES.Invoice.InvoiceErrorCode.V_EXPIRED_SENDER_CERTIFICATE => InvoiceErrorCode.ExpiredSenderCertificate,
            DTOs.SignES.Invoice.InvoiceErrorCode.V_EXPIRED_SIGNER_CERTIFICATE => InvoiceErrorCode.ExpiredSignerCertificate,
            DTOs.SignES.Invoice.InvoiceErrorCode.V_MISSING_OR_INCORRECT_DATA => InvoiceErrorCode.MissingOrIncorrectData,
            DTOs.SignES.Invoice.InvoiceErrorCode.V_MESSAGE_TOO_LONG => InvoiceErrorCode.MessageTooLong,
            DTOs.SignES.Invoice.InvoiceErrorCode.V_INVOICE_NOT_REGISTERED => InvoiceErrorCode.InvoiceNotRegistered,
            DTOs.SignES.Invoice.InvoiceErrorCode.V_INVOICE_ALREADY_CORRECTED => InvoiceErrorCode.InvoiceAlreadyCorrected,
            DTOs.SignES.Invoice.InvoiceErrorCode.V_INVOICE_ALREADY_CANCELLED_CERT_ERROR => InvoiceErrorCode.InvoiceAlreadyCancelledCertError,
            DTOs.SignES.Invoice.InvoiceErrorCode.V_FULL_AMOUNT_MISMATCH => InvoiceErrorCode.FullAmountMismatch,
            DTOs.SignES.Invoice.InvoiceErrorCode.V_ISSUE_DATE_OUT_OF_RANGE => InvoiceErrorCode.IssueDateOutOfRange,
            DTOs.SignES.Invoice.InvoiceErrorCode.V_INVALID_VAT_RATE => InvoiceErrorCode.InvalidVatRate,
            DTOs.SignES.Invoice.InvoiceErrorCode.V_INTERNATIONAL_RECIPIENT_SPAIN_ID_TYPE_ERROR => InvoiceErrorCode.InternationalRecipientSpainIdTypeError,
            DTOs.SignES.Invoice.InvoiceErrorCode.V_INCOMPATIBLE_VAT_SYSTEMS => InvoiceErrorCode.IncompatibleVatSystems,
            DTOs.SignES.Invoice.InvoiceErrorCode.V_TOO_MANY_VAT_SYSTEMS => InvoiceErrorCode.TooManyVatSystems,
            DTOs.SignES.Invoice.InvoiceErrorCode.V_INCORRECT_ITEM_VAT_CALCULATION => InvoiceErrorCode.IncorrectItemVatCalculation,
            DTOs.SignES.Invoice.InvoiceErrorCode.V_INVALID_CORRECTION_TYPE_FOR_COUPON => InvoiceErrorCode.InvalidCorrectionTypeForCoupon,
            DTOs.SignES.Invoice.InvoiceErrorCode.V_REGISTRATION_REMEDY_ALREADY_EXISTS => InvoiceErrorCode.RegistrationRemedyAlreadyExists,
            DTOs.SignES.Invoice.InvoiceErrorCode.V_FILE_TO_REMEDY_DOES_NOT_EXIST => InvoiceErrorCode.FileToRemedyDoesNotExist,
            DTOs.SignES.Invoice.InvoiceErrorCode.V_CANCELLATION_REMEDY_ALREADY_EXISTS => InvoiceErrorCode.CancellationRemedyAlreadyExists,
            DTOs.SignES.Invoice.InvoiceErrorCode.V_CANNOT_REMEDY_BASIC_DATA => InvoiceErrorCode.CannotRemedyBasicData,
            _ => throw new ArgumentOutOfRangeException(nameof(code), code, null)
        };
    }
}