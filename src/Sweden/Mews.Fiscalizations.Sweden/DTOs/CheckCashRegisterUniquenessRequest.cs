using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Sweden.DTOs;

internal sealed class CheckCashRegisterUniquenessRequest
{
    [Required]
    [JsonPropertyName("country")]
    public required string CountryCode { get; init; }

    [Required]
    [JsonPropertyName("corporateId")]
    public required string CorporateId { get; init; }

    [Required]
    [JsonPropertyName("cashRegisterName")]
    public required string CashRegisterName { get; init; }
}