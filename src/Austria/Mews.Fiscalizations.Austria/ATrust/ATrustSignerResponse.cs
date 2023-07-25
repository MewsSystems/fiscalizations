using Newtonsoft.Json;

namespace Mews.Fiscalizations.Austria.ATrust;

public sealed class ATrustSignerResponse
{
    [JsonProperty("result")]
    public string JwsRepresentation { get; set; }

    [JsonProperty("alg")]
    public string Algorithm { get; set; }
}