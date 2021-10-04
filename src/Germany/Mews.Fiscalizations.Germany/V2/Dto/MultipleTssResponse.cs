using System.Collections.Generic;
using Newtonsoft.Json;

namespace Mews.Fiscalizations.Germany.V2.Dto
{
    internal sealed class MultipleTssResponse
    {
        [JsonProperty("data")]
        public IEnumerable<TssResponse> TssList { get; set; }
    }
}
