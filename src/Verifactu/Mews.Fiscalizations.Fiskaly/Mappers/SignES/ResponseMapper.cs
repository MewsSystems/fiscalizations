using Mews.Fiscalizations.Fiskaly.DTOs;
using Mews.Fiscalizations.Fiskaly.DTOs.SignES.Audit;
using Mews.Fiscalizations.Fiskaly.DTOs.SignES.Auth;
using Mews.Fiscalizations.Fiskaly.DTOs.SignES.Client;
using Mews.Fiscalizations.Fiskaly.DTOs.SignES.Signer;
using Mews.Fiscalizations.Fiskaly.DTOs.SignES.Taxpayer;
using Mews.Fiscalizations.Fiskaly.Models;
using Mews.Fiscalizations.Fiskaly.Models.SignES.Audit;
using Mews.Fiscalizations.Fiskaly.Models.SignES.Clients;
using Mews.Fiscalizations.Fiskaly.Models.SignES.Invoice;
using Mews.Fiscalizations.Fiskaly.Models.SignES.Taxpayer;
using FiskalyEnvironment = Mews.Fiscalizations.Fiskaly.Models.FiskalyEnvironment;
using Signer = Mews.Fiscalizations.Fiskaly.Models.SignES.Signer.Signer;
using SignerCertificate = Mews.Fiscalizations.Fiskaly.Models.SignES.Signer.SignerCertificate;
using TaxpayerState = Mews.Fiscalizations.Fiskaly.Models.SignES.Taxpayer.TaxpayerState;
using TaxpayerType = Mews.Fiscalizations.Fiskaly.Models.SignES.Taxpayer.TaxpayerType;

namespace Mews.Fiscalizations.Fiskaly.Mappers.SignES;

internal static class ResponseMapper
{
    public static ErrorResult FromDto(this FiskalyErrorResponse errorResponse)
    {
        return new ErrorResult(
            Status: errorResponse.StatusCode,
            Code: errorResponse.Code,
            Error: errorResponse.Error,
            Message: errorResponse.Message
        );
    }

    public static Taxpayer FromDto(this TaxpayerResponse response)
    {
        return new Taxpayer(
            LegalName: response.Content.Issuer.LegalName,
            TaxIdentifier: response.Content.Issuer.TaxNumber,
            Territory: response.Content.Territory.FromDto(),
            Type: response.Content.Type.FromDto(),
            State: response.Content.State?.FromDto() ?? Models.SignES.Taxpayer.TaxpayerState.Enabled
        );
    }

    public static Signer FromDto(this SignerResponse response)
    {
        var cert = response.SignerData.Certificate;
        return new Signer(
            Id: response.SignerData.Id,
            Certificate: new SignerCertificate(cert.SerialNumber, cert.X509Pem, cert.ExpiresAt)
        );
    }

    public static ClientDevice FromDto(this ClientResponse response)
    {
        return new ClientDevice(
            ClientId: response.ClientData.Id,
            SignerId: response.ClientData.Signer.Id
        );
    }

    public static SoftwareAuditData FromDto(this SoftwareResponse response)
    {
        return new SoftwareAuditData(
            CompanyLegalName: response.Data.Company.LegalName,
            CompanyTaxIdentifier: response.Data.Company.TaxNumber,
            SoftwareName: response.Data.Name,
            License: response.Data.License,
            Version: response.Data.Version,
            ResponsibilityDeclaration: response.Data.ResponsibilityDeclaration ?? ""
        );
    }

    public static InvoiceResponse FromDto(this DTOs.SignES.Invoice.InvoiceResponse response)
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
            State: response.Content.State.FromDto(),
            Transmission: new SignedInvoiceTransmission(
                RegistrationState: response.Content.Transmission.Registration.FromDto(),
                CancellationState: response.Content.Transmission.Cancellation.FromDto()
            ),
            Validations: response.Content.Validations.Select(v => new InvoiceValidationData(
                ErrorCode: v.Code.FromDto(),
                Description: v.Description
            ))
        );
    }

    private static InvoiceState FromDto(this DTOs.SignES.Invoice.InvoiceState state)
    {
        return state switch
        {
            DTOs.SignES.Invoice.InvoiceState.ISSUED => InvoiceState.Issued,
            DTOs.SignES.Invoice.InvoiceState.CANCELLED => InvoiceState.Canceled,
            DTOs.SignES.Invoice.InvoiceState.IMPORTED => InvoiceState.Imported,
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
        };
    }

    private static SignedInvoiceRegistrationState FromDto(this DTOs.SignES.Invoice.SignedInvoiceRegistrationState state)
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

    private static SignedInvoiceCancellationState FromDto(this DTOs.SignES.Invoice.SignedInvoiceCancellationState state)
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

    private static InvoiceErrorCode FromDto(this DTOs.SignES.Invoice.InvoiceErrorCode code)
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

    private static TaxpayerType FromDto(this DTOs.SignES.Taxpayer.TaxpayerType type)
    {
        return type switch
        {
            DTOs.SignES.Taxpayer.TaxpayerType.INDIVIDUAL => TaxpayerType.Individual,
            DTOs.SignES.Taxpayer.TaxpayerType.COMPANY => TaxpayerType.Company,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    private static TaxpayerState FromDto(this DTOs.SignES.Taxpayer.TaxpayerState state)
    {
        return state switch
        {
            DTOs.SignES.Taxpayer.TaxpayerState.ENABLED => TaxpayerState.Enabled,
            DTOs.SignES.Taxpayer.TaxpayerState.DISABLED => TaxpayerState.Disabled,
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
        };
    }

    private static TaxpayerTerritory FromDto(this Territory territory)
    {
        return territory switch
        {
            Territory.ARABA => TaxpayerTerritory.Araba,
            Territory.BIZKAIA => TaxpayerTerritory.Bizkaia,
            Territory.GIPUZKOA => TaxpayerTerritory.Gipuzkoa,
            Territory.NAVARRE => TaxpayerTerritory.Navarre,
            Territory.CANARY_ISLANDS => TaxpayerTerritory.CanaryIslands,
            Territory.CEUTA => TaxpayerTerritory.Ceuta,
            Territory.MELILLA => TaxpayerTerritory.Melilla,
            Territory.SPAIN_OTHER => TaxpayerTerritory.SpainOther,
            _ => throw new ArgumentOutOfRangeException(nameof(territory), territory, null)
        };
    }

    
}