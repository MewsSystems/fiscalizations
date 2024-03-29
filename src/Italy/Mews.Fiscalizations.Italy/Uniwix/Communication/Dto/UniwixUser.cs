using Newtonsoft.Json;

namespace Mews.Fiscalizations.Italy.Uniwix.Communication.Dto;

public class UniwixUser
{
    public string Id { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    [JsonProperty("bridge_key")]
    public string BridgeKey { get; set; }

    [JsonProperty("piva")]
    public string TaxIdentificationNumber { get; set; }

    [JsonProperty("descrizione")]
    public string Description { get; set; }
}