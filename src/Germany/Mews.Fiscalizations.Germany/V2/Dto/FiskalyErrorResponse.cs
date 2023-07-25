using Newtonsoft.Json;

namespace Mews.Fiscalizations.Germany.V2.Dto;

internal sealed class FiskalyErrorResponse
{
    [JsonProperty("status_code")]
    public int StatusCode { get; set; }

    [JsonProperty("error")]
    public string Error { get; set; }

    [JsonProperty("code")]
    public string Code { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }
}