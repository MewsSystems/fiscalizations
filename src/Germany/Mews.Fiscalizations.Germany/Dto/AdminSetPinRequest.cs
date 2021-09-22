using Newtonsoft.Json;

namespace Mews.Fiscalizations.Germany.Dto
{
    internal sealed class AdminSetPinRequest
    {
        [JsonProperty("admin_puk")]
        public string AdminPuk { get; set; }

        [JsonProperty("new_admin_pin")]
        public string NewAdminPin { get; set; }
    }
}
