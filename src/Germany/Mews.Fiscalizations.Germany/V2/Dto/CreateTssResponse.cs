using Newtonsoft.Json;

namespace Mews.Fiscalizations.Germany.V2.Dto
{
    internal sealed class CreateTssResponse : TssResponse
    {
        [JsonProperty("admin_puk")]
        public string AdminPuk { get; set; }
    }
}