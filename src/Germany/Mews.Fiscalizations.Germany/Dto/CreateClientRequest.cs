using Newtonsoft.Json;

namespace Mews.Fiscalizations.Germany.Dto
{
    public class CreateClientRequest
    {
        [JsonProperty("serial_number")]
        public string SerialNumber { get; set; }
    }
}
