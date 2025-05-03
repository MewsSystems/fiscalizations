namespace Mews.Fiscalizations.Fiskaly.Models;

public sealed record InvoiceResponse(
    Guid InvoiceId,
    Guid SignerId,
    Guid ClientId,
    string InvoiceJsonData,
    InvoiceComplianceData ComplianceData,
    InvoiceState State,
    SignedInvoiceTransmission Transmission,
    IEnumerable<InvoiceValidationData> Validations
);

public sealed record InvoiceComplianceData(
    string Base64ImageData,
    string ImageFormat,
    string VerifactuValidationUrl,
    string VerifactuInvoiceText
);

public sealed record SignedInvoiceTransmission(
    SignedInvoiceRegistrationState RegistrationState,
    SignedInvoiceCancellationState CancellationState
);

public sealed record InvoiceValidationData(InvoiceErrorCode ErrorCode, string Description);

public enum InvoiceState
{
    Issued = 0,
    Canceled = 1,
    Imported = 2
}

public enum SignedInvoiceRegistrationState
{
    Pending = 0,
    Registered = 1,
    RequiresCorrection = 2,
    RequiresInspection = 3,
    Stored = 4,
    Invalid = 5,
}

public enum SignedInvoiceCancellationState
{
    NotCancelled = 0,
    Pending = 1,
    Cancelled = 2,
    RequiresInspection = 3,
    Stored = 4,
    Invalid = 5
}

public enum InvoiceErrorCode
{
    IncorrectSenderCertificate = 0,
    XsdSchemaNotComply = 1,
    InvoiceWithoutLines = 2,
    RequiredFieldIncorrectOrMissing = 3,
    InvoiceAlreadyRegistered = 4,
    ServiceNotAvailable = 5,
    InvalidSenderCertificate = 6,
    WrongSignature = 7,
    IncorrectInvoiceChaining = 8,
    BusinessValidationError = 9,
    DeviceNotRegistered = 10,
    ExpiredSignatureCertificate = 11,
    ExpiredSenderCertificate = 12,
    ExpiredSignerCertificate = 13,
    MissingOrIncorrectData = 14,
    MessageTooLong = 15,
    InvoiceNotRegistered = 16,
    InvoiceAlreadyCancelled = 17,
    InvoiceAlreadyCorrected = 18,
    InvoiceAlreadyCancelledCertError = 19,
    FullAmountMismatch = 20,
    IssueDateOutOfRange = 21,
    InvalidVatRate = 22,
    InternationalRecipientSpainIdTypeError = 23,
    IncompatibleVatSystems = 24,
    TooManyVatSystems = 25,
    IncorrectItemVatCalculation = 26,
    InvalidCorrectionTypeForCoupon = 27,
    RegistrationRemedyAlreadyExists = 28,
    FileToRemedyDoesNotExist = 29,
    CancellationRemedyAlreadyExists = 30,
    CannotRemedyBasicData = 31
}