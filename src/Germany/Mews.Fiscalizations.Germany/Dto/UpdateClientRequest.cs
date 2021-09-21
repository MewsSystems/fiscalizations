using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Mews.Fiscalizations.Germany.Dto
{
    internal class UpdateClientRequest
    {
        [JsonProperty("state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ClientState State { get; set; }
    }
}
