using Newtonsoft.Json;
using System;

namespace Mews.Fiscalizations.Germany.Dto
{
    public sealed class TssRequest
    {
        [JsonProperty("tss_id")]
        public Guid TssId { get; set; }
    }
}
