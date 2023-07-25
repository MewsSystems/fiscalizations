using Newtonsoft.Json;

namespace Mews.Fiscalizations.Germany.V2.Dto;

internal sealed class AdminLoginRequest
{
    [JsonProperty("admin_pin")]
    public string AdminPin { get; set; }
}