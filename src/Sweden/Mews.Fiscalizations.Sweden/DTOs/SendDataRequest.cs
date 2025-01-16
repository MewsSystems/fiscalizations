using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Sweden.DTOs;

internal sealed class SendDataRequest
{
    [Required]
    [JsonPropertyName("brutto")]
    public required int GrossAmount { get; init; }

    [Required]
    [JsonPropertyName("vatRateToSum")]
    public required IReadOnlyDictionary<string, int> VatRateToSum { get; init; }

    [Required]
    [JsonPropertyName("refund")]
    public required bool IsRefund { get; init; }

    [Required]
    [JsonPropertyName("printType")]
    public required PrintType PrintType { get; init; }

    [JsonPropertyName("receiptNumber")]
    public int? ReceiptNumber { get; init; }

    [Required]
    [JsonPropertyName("date")]
    public long UnixTimestamp { get; init; }
}

internal enum PrintType
{
    Normal = 0,
    Copy = 1,
    Proforma = 2
}