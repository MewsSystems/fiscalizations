using System.Globalization;
using Mews.Fiscalizations.Fiskaly.DTOs.SignES.Invoices;
using Mews.Fiscalizations.Fiskaly.Models.SignES.Invoices;
using InvoiceErrorCode = Mews.Fiscalizations.Fiskaly.Models.SignES.Invoices.InvoiceErrorCode;
using InvoiceResponse = Mews.Fiscalizations.Fiskaly.Models.SignES.Invoices.InvoiceResponse;
using InvoiceState = Mews.Fiscalizations.Fiskaly.Models.SignES.Invoices.InvoiceState;
using SignedInvoiceCancellationState = Mews.Fiscalizations.Fiskaly.Models.SignES.Invoices.SignedInvoiceCancellationState;
using SignedInvoiceRegistrationState = Mews.Fiscalizations.Fiskaly.Models.SignES.Invoices.SignedInvoiceRegistrationState;
using TaxExemptionReason = Mews.Fiscalizations.Fiskaly.Models.SignES.Invoices.TaxExemptionReason;

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

    private static InvoiceErrorCode MapInvoiceErrorCode(this DTOs.SignES.Invoices.InvoiceErrorCode code)
    {
        return code switch
        {
            DTOs.SignES.Invoices.InvoiceErrorCode.V_INCORRECT_SENDER_CERTIFICATE => InvoiceErrorCode.IncorrectSenderCertificate,
            DTOs.SignES.Invoices.InvoiceErrorCode.V_XSD_SCHEMA_NOT_COMPLY => InvoiceErrorCode.XsdSchemaNotComply,
            DTOs.SignES.Invoices.InvoiceErrorCode.V_INVOICE_WITHOUT_LINES => InvoiceErrorCode.InvoiceWithoutLines,
            DTOs.SignES.Invoices.InvoiceErrorCode.V_REQUIRED_FIELD_MISSING_OR_INCORRECT => InvoiceErrorCode.RequiredFieldIncorrectOrMissing,
            DTOs.SignES.Invoices.InvoiceErrorCode.V_INVOICE_ALREADY_REGISTERED => InvoiceErrorCode.InvoiceAlreadyRegistered,
            DTOs.SignES.Invoices.InvoiceErrorCode.V_INVOICE_ALREADY_CANCELLED => InvoiceErrorCode.InvoiceAlreadyCancelled,
            DTOs.SignES.Invoices.InvoiceErrorCode.V_SERVICE_NOT_AVAILABLE => InvoiceErrorCode.ServiceNotAvailable,
            DTOs.SignES.Invoices.InvoiceErrorCode.V_INVALID_SENDER_CERTIFICATE => InvoiceErrorCode.InvalidSenderCertificate,
            DTOs.SignES.Invoices.InvoiceErrorCode.V_WRONG_SIGNATURE => InvoiceErrorCode.WrongSignature,
            DTOs.SignES.Invoices.InvoiceErrorCode.V_INCORRECT_INVOICE_CHAINING => InvoiceErrorCode.IncorrectInvoiceChaining,
            DTOs.SignES.Invoices.InvoiceErrorCode.V_BUSINESS_VALIDATION_DATA_ERROR => InvoiceErrorCode.BusinessValidationError,
            DTOs.SignES.Invoices.InvoiceErrorCode.V_DEVICE_NOT_REGISTERED => InvoiceErrorCode.DeviceNotRegistered,
            DTOs.SignES.Invoices.InvoiceErrorCode.V_EXPIRED_SIGNATURE_CERTIFICATE => InvoiceErrorCode.ExpiredSignatureCertificate,
            DTOs.SignES.Invoices.InvoiceErrorCode.V_EXPIRED_SENDER_CERTIFICATE => InvoiceErrorCode.ExpiredSenderCertificate,
            DTOs.SignES.Invoices.InvoiceErrorCode.V_EXPIRED_SIGNER_CERTIFICATE => InvoiceErrorCode.ExpiredSignerCertificate,
            DTOs.SignES.Invoices.InvoiceErrorCode.V_MISSING_OR_INCORRECT_DATA => InvoiceErrorCode.MissingOrIncorrectData,
            DTOs.SignES.Invoices.InvoiceErrorCode.V_MESSAGE_TOO_LONG => InvoiceErrorCode.MessageTooLong,
            DTOs.SignES.Invoices.InvoiceErrorCode.V_INVOICE_NOT_REGISTERED => InvoiceErrorCode.InvoiceNotRegistered,
            DTOs.SignES.Invoices.InvoiceErrorCode.V_INVOICE_ALREADY_CORRECTED => InvoiceErrorCode.InvoiceAlreadyCorrected,
            DTOs.SignES.Invoices.InvoiceErrorCode.V_INVOICE_ALREADY_CANCELLED_CERT_ERROR => InvoiceErrorCode.InvoiceAlreadyCancelledCertError,
            DTOs.SignES.Invoices.InvoiceErrorCode.V_FULL_AMOUNT_MISMATCH => InvoiceErrorCode.FullAmountMismatch,
            DTOs.SignES.Invoices.InvoiceErrorCode.V_ISSUE_DATE_OUT_OF_RANGE => InvoiceErrorCode.IssueDateOutOfRange,
            DTOs.SignES.Invoices.InvoiceErrorCode.V_INVALID_VAT_RATE => InvoiceErrorCode.InvalidVatRate,
            DTOs.SignES.Invoices.InvoiceErrorCode.V_INTERNATIONAL_RECIPIENT_SPAIN_ID_TYPE_ERROR => InvoiceErrorCode.InternationalRecipientSpainIdTypeError,
            DTOs.SignES.Invoices.InvoiceErrorCode.V_INCOMPATIBLE_VAT_SYSTEMS => InvoiceErrorCode.IncompatibleVatSystems,
            DTOs.SignES.Invoices.InvoiceErrorCode.V_TOO_MANY_VAT_SYSTEMS => InvoiceErrorCode.TooManyVatSystems,
            DTOs.SignES.Invoices.InvoiceErrorCode.V_INCORRECT_ITEM_VAT_CALCULATION => InvoiceErrorCode.IncorrectItemVatCalculation,
            DTOs.SignES.Invoices.InvoiceErrorCode.V_INVALID_CORRECTION_TYPE_FOR_COUPON => InvoiceErrorCode.InvalidCorrectionTypeForCoupon,
            DTOs.SignES.Invoices.InvoiceErrorCode.V_REGISTRATION_REMEDY_ALREADY_EXISTS => InvoiceErrorCode.RegistrationRemedyAlreadyExists,
            DTOs.SignES.Invoices.InvoiceErrorCode.V_FILE_TO_REMEDY_DOES_NOT_EXIST => InvoiceErrorCode.FileToRemedyDoesNotExist,
            DTOs.SignES.Invoices.InvoiceErrorCode.V_CANCELLATION_REMEDY_ALREADY_EXISTS => InvoiceErrorCode.CancellationRemedyAlreadyExists,
            DTOs.SignES.Invoices.InvoiceErrorCode.V_CANNOT_REMEDY_BASIC_DATA => InvoiceErrorCode.CannotRemedyBasicData,
            _ => throw new ArgumentOutOfRangeException(nameof(code), code, null)
        };
    }
}