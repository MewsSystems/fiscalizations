using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Mews.Fiscalizations.Germany.V2.Dto
{
    internal sealed class UpdateClientRequest
    {
        [JsonProperty("state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ClientState State { get; set; }
    }
}
