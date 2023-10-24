using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Basque.Dto.Bizkaia.Header;

public class BizkaiaHeaderData
{
    [JsonPropertyName("con")]
    public string With { get; set; }
    
    [JsonPropertyName("apa")]
    public string Section { get; set; }

    [JsonPropertyName("inte")]
    public Inte Inte { get; set; }

    [JsonPropertyName("drs")]
    public Drs Drs { get; set; }
}

public class Drs
{
    [JsonPropertyName("mode")]
    public string Mode { get; set; }

    [JsonPropertyName("ejer")]
    public string FiscalYear { get; set; }
}

public class Inte
{
    [JsonPropertyName("nif")]
    public string TaxpayerIdentificationNumber { get; set; }

    [JsonPropertyName("nrs")]
    public string Name { get; set; }

    [JsonPropertyName("ap1")]
    public string FamilyName1 { get; set; }

    [JsonPropertyName("ap2")]
    public string FamiliyName2 { get; set; }
}
