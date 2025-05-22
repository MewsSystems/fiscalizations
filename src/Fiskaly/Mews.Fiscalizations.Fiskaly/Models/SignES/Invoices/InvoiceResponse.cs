namespace Mews.Fiscalizations.Fiskaly.Models.SignES.Invoices;

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

public sealed record InvoiceValidationData(string ErrorCode, string Description);

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