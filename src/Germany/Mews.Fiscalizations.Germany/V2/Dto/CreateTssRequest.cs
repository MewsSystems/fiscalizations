using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Mews.Fiscalizations.Germany.V2.Dto
{
    public sealed class CreateTssRequest
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public TssState State { get; set; }
    }
}
