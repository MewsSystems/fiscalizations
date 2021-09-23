using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Mews.Fiscalizations.Germany.V2.Dto
{
    public sealed class UpdateTssRequest
    {
        [JsonProperty("state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public TssState State { get; set; }
    }
}