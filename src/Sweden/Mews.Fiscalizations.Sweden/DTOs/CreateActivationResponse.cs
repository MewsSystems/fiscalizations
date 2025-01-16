using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Sweden.DTOs;

internal sealed class CreateActivationResponse
{
    [Required]
    [JsonPropertyName("apiKey")]
    public string ApiKey { get; set; }

    [JsonPropertyName("productionNumber")]
    public string ProductionNumber { get; set; }

    [Required]
    [JsonPropertyName("activationId")]
    public int ActivationId { get; set; }

    [JsonPropertyName("address")]
    public string Address { get; set; }

    [JsonPropertyName("zip")]
    public string Zip { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("companyName")]
    public string CompanyName { get; set; }

    [JsonPropertyName("phone")]
    public string Phone { get; set; }
}