using Newtonsoft.Json;
using System;

namespace Mews.Fiscalization.Germany.Dto
{
    public sealed class TssRequest
    {
        [JsonProperty("tss_id")]
        public Guid TssId { get; set; }
    }
}
