using Newtonsoft.Json;

namespace Mews.Fiscalizations.Germany.Dto
{
    internal sealed class AdminLoginRequest
    {
        [JsonProperty("admin_pin")]
        public string AdminPin { get; set; }
    }
}
