using Mews.Fiscalizations.Verifactu.Models;

namespace Mews.Fiscalizations.Verifactu;

internal static class ResponseMapper
{
    public static AccessToken FromDto(this DTOs.AuthorizationTokenResponse response)
    {
        return new AccessToken(
            value: response.Data.AccessToken.Bearer,
            environment: response.Data.Claims.Environment.FromDto(),
            expirationUtc: response.Data.AccessToken.ExpiresAt.FromUnixTime()
        );
    }

    public static ErrorResult FromDto(this DTOs.FiskalyErrorResponse errorResponse)
    {
        return new ErrorResult(
            Status: errorResponse.StatusCode,
            Code: errorResponse.Code,
            Error: errorResponse.Error,
            Message: errorResponse.Message
        );
    }

    public static Taxpayer FromDto(this DTOs.TaxpayerResponse response)
    {
        return new Taxpayer(
            LegalName: response.Content.Issuer.LegalName,
            TaxIdentifier: response.Content.Issuer.TaxNumber,
            Territory: response.Content.Territory.FromDto(),
            Type: response.Content.Type.FromDto(),
            State: response.Content.State?.FromDto() ?? TaxpayerState.Enabled
        );
    }

    public static Signer FromDto(this DTOs.SignerResponse response)
    {
        var cert = response.SignerData.Certificate;
        return new Signer(
            Id: response.SignerData.Id,
            Certificate: new SignerCertificate(cert.SerialNumber, cert.X509Pem, cert.ExpiresAt)
        );
    }

    public static ClientDevice FromDto(this DTOs.ClientResponse response)
    {
        return new ClientDevice(
            ClientId: response.ClientData.Id,
            SignerId: response.ClientData.Signer.Id
        );
    }

    public static SoftwareAuditData FromDto(this DTOs.SoftwareResponse response)
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

    public static InvoiceResponse FromDto(this DTOs.InvoiceResponse response)
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

    private static InvoiceState FromDto(this DTOs.InvoiceState state)
    {
        return state switch
        {
            DTOs.InvoiceState.ISSUED => InvoiceState.Issued,
            DTOs.InvoiceState.CANCELLED => InvoiceState.Canceled,
            DTOs.InvoiceState.IMPORTED => InvoiceState.Imported,
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
        };
    }

    private static SignedInvoiceRegistrationState FromDto(this DTOs.SignedInvoiceRegistrationState state)
    {
        return state switch
        {
            DTOs.SignedInvoiceRegistrationState.PENDING => SignedInvoiceRegistrationState.Pending,
            DTOs.SignedInvoiceRegistrationState.REGISTERED => SignedInvoiceRegistrationState.Registered,
            DTOs.SignedInvoiceRegistrationState.REQUIRES_CORRECTION => SignedInvoiceRegistrationState.RequiresCorrection,
            DTOs.SignedInvoiceRegistrationState.REQUIRES_INSPECTION => SignedInvoiceRegistrationState.RequiresInspection,
            DTOs.SignedInvoiceRegistrationState.STORED => SignedInvoiceRegistrationState.Stored,
            DTOs.SignedInvoiceRegistrationState.INVALID => SignedInvoiceRegistrationState.Invalid,
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
        };
    }

    private static SignedInvoiceCancellationState FromDto(this DTOs.SignedInvoiceCancellationState state)
    {
        return state switch
        {
            DTOs.SignedInvoiceCancellationState.NOT_CANCELLED => SignedInvoiceCancellationState.NotCancelled,
            DTOs.SignedInvoiceCancellationState.PENDING => SignedInvoiceCancellationState.Pending,
            DTOs.SignedInvoiceCancellationState.CANCELLED => SignedInvoiceCancellationState.Cancelled,
            DTOs.SignedInvoiceCancellationState.REQUIRES_INSPECTION => SignedInvoiceCancellationState.RequiresInspection,
            DTOs.SignedInvoiceCancellationState.STORED => SignedInvoiceCancellationState.Stored,
            DTOs.SignedInvoiceCancellationState.INVALID => SignedInvoiceCancellationState.Invalid,
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
        };
    }

    private static InvoiceErrorCode FromDto(this DTOs.InvoiceErrorCode code)
    {
        return code switch
        {
            DTOs.InvoiceErrorCode.V_INCORRECT_SENDER_CERTIFICATE => InvoiceErrorCode.IncorrectSenderCertificate,
            DTOs.InvoiceErrorCode.V_XSD_SCHEMA_NOT_COMPLY => InvoiceErrorCode.XsdSchemaNotComply,
            DTOs.InvoiceErrorCode.V_INVOICE_WITHOUT_LINES => InvoiceErrorCode.InvoiceWithoutLines,
            DTOs.InvoiceErrorCode.V_REQUIRED_FIELD_MISSING_OR_INCORRECT => InvoiceErrorCode.RequiredFieldIncorrectOrMissing,
            DTOs.InvoiceErrorCode.V_INVOICE_ALREADY_REGISTERED => InvoiceErrorCode.InvoiceAlreadyRegistered,
            DTOs.InvoiceErrorCode.V_INVOICE_ALREADY_CANCELLED => InvoiceErrorCode.InvoiceAlreadyCancelled,
            DTOs.InvoiceErrorCode.V_SERVICE_NOT_AVAILABLE => InvoiceErrorCode.ServiceNotAvailable,
            DTOs.InvoiceErrorCode.V_INVALID_SENDER_CERTIFICATE => InvoiceErrorCode.InvalidSenderCertificate,
            DTOs.InvoiceErrorCode.V_WRONG_SIGNATURE => InvoiceErrorCode.WrongSignature,
            DTOs.InvoiceErrorCode.V_INCORRECT_INVOICE_CHAINING => InvoiceErrorCode.IncorrectInvoiceChaining,
            DTOs.InvoiceErrorCode.V_BUSINESS_VALIDATION_DATA_ERROR => InvoiceErrorCode.BusinessValidationError,
            DTOs.InvoiceErrorCode.V_DEVICE_NOT_REGISTERED => InvoiceErrorCode.DeviceNotRegistered,
            DTOs.InvoiceErrorCode.V_EXPIRED_SIGNATURE_CERTIFICATE => InvoiceErrorCode.ExpiredSignatureCertificate,
            DTOs.InvoiceErrorCode.V_EXPIRED_SENDER_CERTIFICATE => InvoiceErrorCode.ExpiredSenderCertificate,
            DTOs.InvoiceErrorCode.V_EXPIRED_SIGNER_CERTIFICATE => InvoiceErrorCode.ExpiredSignerCertificate,
            DTOs.InvoiceErrorCode.V_MISSING_OR_INCORRECT_DATA => InvoiceErrorCode.MissingOrIncorrectData,
            DTOs.InvoiceErrorCode.V_MESSAGE_TOO_LONG => InvoiceErrorCode.MessageTooLong,
            DTOs.InvoiceErrorCode.V_INVOICE_NOT_REGISTERED => InvoiceErrorCode.InvoiceNotRegistered,
            DTOs.InvoiceErrorCode.V_INVOICE_ALREADY_CORRECTED => InvoiceErrorCode.InvoiceAlreadyCorrected,
            DTOs.InvoiceErrorCode.V_INVOICE_ALREADY_CANCELLED_CERT_ERROR => InvoiceErrorCode.InvoiceAlreadyCancelledCertError,
            DTOs.InvoiceErrorCode.V_FULL_AMOUNT_MISMATCH => InvoiceErrorCode.FullAmountMismatch,
            DTOs.InvoiceErrorCode.V_ISSUE_DATE_OUT_OF_RANGE => InvoiceErrorCode.IssueDateOutOfRange,
            DTOs.InvoiceErrorCode.V_INVALID_VAT_RATE => InvoiceErrorCode.InvalidVatRate,
            DTOs.InvoiceErrorCode.V_INTERNATIONAL_RECIPIENT_SPAIN_ID_TYPE_ERROR => InvoiceErrorCode.InternationalRecipientSpainIdTypeError,
            DTOs.InvoiceErrorCode.V_INCOMPATIBLE_VAT_SYSTEMS => InvoiceErrorCode.IncompatibleVatSystems,
            DTOs.InvoiceErrorCode.V_TOO_MANY_VAT_SYSTEMS => InvoiceErrorCode.TooManyVatSystems,
            DTOs.InvoiceErrorCode.V_INCORRECT_ITEM_VAT_CALCULATION => InvoiceErrorCode.IncorrectItemVatCalculation,
            DTOs.InvoiceErrorCode.V_INVALID_CORRECTION_TYPE_FOR_COUPON => InvoiceErrorCode.InvalidCorrectionTypeForCoupon,
            DTOs.InvoiceErrorCode.V_REGISTRATION_REMEDY_ALREADY_EXISTS => InvoiceErrorCode.RegistrationRemedyAlreadyExists,
            DTOs.InvoiceErrorCode.V_FILE_TO_REMEDY_DOES_NOT_EXIST => InvoiceErrorCode.FileToRemedyDoesNotExist,
            DTOs.InvoiceErrorCode.V_CANCELLATION_REMEDY_ALREADY_EXISTS => InvoiceErrorCode.CancellationRemedyAlreadyExists,
            DTOs.InvoiceErrorCode.V_CANNOT_REMEDY_BASIC_DATA => InvoiceErrorCode.CannotRemedyBasicData,
            _ => throw new ArgumentOutOfRangeException(nameof(code), code, null)
        };
    }

    private static TaxpayerType FromDto(this DTOs.TaxpayerType type)
    {
        return type switch
        {
            DTOs.TaxpayerType.INDIVIDUAL => TaxpayerType.Individual,
            DTOs.TaxpayerType.COMPANY => TaxpayerType.Company,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    private static TaxpayerState FromDto(this DTOs.TaxpayerState state)
    {
        return state switch
        {
            DTOs.TaxpayerState.ENABLED => TaxpayerState.Enabled,
            DTOs.TaxpayerState.DISABLED => TaxpayerState.Disabled,
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
        };
    }

    private static TaxpayerTerritory FromDto(this DTOs.Territory territory)
    {
        return territory switch
        {
            DTOs.Territory.ARABA => TaxpayerTerritory.Araba,
            DTOs.Territory.BIZKAIA => TaxpayerTerritory.Bizkaia,
            DTOs.Territory.GIPUZKOA => TaxpayerTerritory.Gipuzkoa,
            DTOs.Territory.NAVARRE => TaxpayerTerritory.Navarre,
            DTOs.Territory.CANARY_ISLANDS => TaxpayerTerritory.CanaryIslands,
            DTOs.Territory.CEUTA => TaxpayerTerritory.Ceuta,
            DTOs.Territory.MELILLA => TaxpayerTerritory.Melilla,
            DTOs.Territory.SPAIN_OTHER => TaxpayerTerritory.SpainOther,
            _ => throw new ArgumentOutOfRangeException(nameof(territory), territory, null)
        };
    }

    private static FiskalyEnvironment FromDto(this DTOs.FiskalyEnvironment environment)
    {
        return environment switch
        {
            DTOs.FiskalyEnvironment.LIVE => FiskalyEnvironment.Production,
            DTOs.FiskalyEnvironment.TEST => FiskalyEnvironment.Test,
            _ => throw new ArgumentOutOfRangeException(nameof(environment), environment, null)
        };
    }
}