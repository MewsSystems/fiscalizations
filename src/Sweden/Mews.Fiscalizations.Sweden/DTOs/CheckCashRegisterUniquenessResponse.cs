using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Sweden.DTOs;

internal sealed class CheckCashRegisterUniquenessResponse
{
    [Required]
    [JsonPropertyName("exists")]
    public bool Exists { get; set; }
}