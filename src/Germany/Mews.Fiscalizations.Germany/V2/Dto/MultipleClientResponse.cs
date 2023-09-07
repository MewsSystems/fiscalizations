using Newtonsoft.Json;

namespace Mews.Fiscalizations.Germany.V2.Dto;

internal class MultipleClientResponse
{
    [JsonProperty("data")]
    public IEnumerable<ClientResponse> Clients { get; set; }
}