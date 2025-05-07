using System.Globalization;
using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Fiskaly.DTOs.SignES.Invoices;

internal sealed class InvoiceResponse
{
    [JsonPropertyName("content")]
    public InvoiceResponseData Content { get; init; }

    [JsonPropertyName("annotations")]
    public List<Annotation> Annotations { get; init; }
}

internal sealed class InvoiceResponseData
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; }

    [JsonPropertyName("signer")]
    public ClientDevices.Signer Signer { get; init; }

    [JsonPropertyName("client")]
    public Client Client { get; init; }

    [JsonPropertyName("issued_at")]
    public string IssuedAtRaw { get; init; }

    [JsonIgnore]
    public DateTime IssuedAt => DateTime.ParseExact(IssuedAtRaw, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);

    [JsonPropertyName("data")]
    public string Data { get; init; }

    [JsonPropertyName("compliance")]
    public Compliance Compliance { get; init; }

    [JsonPropertyName("correction")]
    public Correction Correction { get; init; }

    [JsonPropertyName("remedy")]
    public Remedy Remedy { get; init; }

    [JsonPropertyName("state")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public InvoiceState State { get; init; }

    [JsonPropertyName("transmission")]
    public Transmission Transmission { get; init; }

    [JsonPropertyName("validations")]
    public List<Validation> Validations { get; init; }
}

internal sealed class Annotation
{
    [JsonPropertyName("type")]
    public string Type { get; init; }

    [JsonPropertyName("activity_code")]
    public string ActivityCode { get; init; }

    [JsonPropertyName("income_tax_amount")]
    public string IncomeTaxAmount { get; init; }

    [JsonPropertyName("pay_collect")]
    public bool PayCollect { get; init; }

    [JsonPropertyName("reason")]
    public string Reason { get; init; }
}

internal sealed class Client
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; }
}

internal sealed class Compliance
{
    [JsonPropertyName("code")]
    public Code Code { get; init; }

    [JsonPropertyName("url")]
    public string Url { get; init; }

    [JsonPropertyName("text")]
    public string Text { get; init; }
}

internal sealed class Code
{
    [JsonPropertyName("type")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public VerifactuRepresentationType Type { get; init; }

    [JsonPropertyName("image")]
    public Image Image { get; init; }
}

internal sealed class Correction
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; }
}

internal sealed class Image
{
    [JsonPropertyName("data")]
    public string Data { get; init; }

    [JsonPropertyName("format")]
    public string Format { get; init; }

    [JsonPropertyName("measurements")]
    public Measurements Measurements { get; init; }
}

internal sealed class Measurements
{
    [JsonPropertyName("height")]
    public int Height { get; init; }

    [JsonPropertyName("width")]
    public int Width { get; init; }

    [JsonPropertyName("unit")]
    public string Unit { get; init; }
}

internal sealed class Remedy
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; }
}

internal sealed class Transmission
{
    [JsonPropertyName("registration")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public SignedInvoiceRegistrationState Registration { get; init; }

    [JsonPropertyName("cancellation")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public SignedInvoiceCancellationState Cancellation { get; init; }
}

internal sealed class Validation
{
    [JsonPropertyName("code")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public InvoiceErrorCode Code { get; init; }

    [JsonPropertyName("description")]
    public string Description { get; init; }
}

internal enum VerifactuRepresentationType
{
    QR_CODE,
    PRINTABLE_CODE
}

internal enum InvoiceState
{
    ISSUED,
    CANCELLED,
    IMPORTED
}

internal enum SignedInvoiceRegistrationState
{
    PENDING,
    REGISTERED,
    STORED,
    REQUIRES_CORRECTION,
    REQUIRES_INSPECTION,
    INVALID
}

internal enum SignedInvoiceCancellationState
{
    NOT_CANCELLED,
    PENDING,
    STORED,
    CANCELLED,
    REQUIRES_INSPECTION,
    INVALID
}

internal enum InvoiceErrorCode
{
    V_INCORRECT_SENDER_CERTIFICATE,
    V_XSD_SCHEMA_NOT_COMPLY,
    V_INVOICE_WITHOUT_LINES,
    V_REQUIRED_FIELD_MISSING_OR_INCORRECT,
    V_INVOICE_ALREADY_REGISTERED,
    V_SERVICE_NOT_AVAILABLE,
    V_INVALID_SENDER_CERTIFICATE,
    V_WRONG_SIGNATURE,
    V_INCORRECT_INVOICE_CHAINING,
    V_BUSINESS_VALIDATION_DATA_ERROR,
    V_DEVICE_NOT_REGISTERED,
    V_EXPIRED_SIGNATURE_CERTIFICATE,
    V_EXPIRED_SENDER_CERTIFICATE,
    V_EXPIRED_SIGNER_CERTIFICATE,
    V_MISSING_OR_INCORRECT_DATA,
    V_MESSAGE_TOO_LONG,
    V_INVOICE_NOT_REGISTERED,
    V_INVOICE_ALREADY_CANCELLED,
    V_INVOICE_ALREADY_CORRECTED,
    V_INVOICE_ALREADY_CANCELLED_CERT_ERROR,
    V_FULL_AMOUNT_MISMATCH,
    V_ISSUE_DATE_OUT_OF_RANGE,
    V_INVALID_VAT_RATE,
    V_INTERNATIONAL_RECIPIENT_SPAIN_ID_TYPE_ERROR,
    V_INCOMPATIBLE_VAT_SYSTEMS,
    V_TOO_MANY_VAT_SYSTEMS,
    V_INCORRECT_ITEM_VAT_CALCULATION,
    V_INVALID_CORRECTION_TYPE_FOR_COUPON,
    V_REGISTRATION_REMEDY_ALREADY_EXISTS,
    V_FILE_TO_REMEDY_DOES_NOT_EXIST,
    V_CANCELLATION_REMEDY_ALREADY_EXISTS,
    V_CANNOT_REMEDY_BASIC_DATA
}