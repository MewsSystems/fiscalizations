using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Fiskaly.DTOs.SignES.Invoices;

internal sealed class CorrectingInvoiceRequest<T>
{
    [JsonPropertyName("type")]
    public string Type => "CORRECTING";

    [JsonPropertyName("id")]
    public Guid InvoiceId { get; init; }

    [JsonPropertyName("invoice")]
    public T Invoice { get; init; }
    
    [JsonPropertyName("method")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public CorrectionMethodEnum? CorrectionMethod { get; init; }
    
    [JsonPropertyName("code")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public CorrectingInvoiceCodeEnum? CorrectingInvoiceCode { get; init; }
    
    [JsonPropertyName("coupon")]
    public bool? Coupon { get; init; }
}

public enum CorrectionMethodEnum
{
    SUBSTITUTION,
    DIFFERENCES
}

public enum CorrectingInvoiceCodeEnum
{
    CORRECTION_1,
    CORRECTION_2,
    CORRECTION_3,
    CORRECTION_4,
}