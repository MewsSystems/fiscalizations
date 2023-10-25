using Newtonsoft.Json;

namespace Mews.Fiscalizations.Basque.Dto.Bizkaia.Header;

public sealed class BizkaiaHeaderData
{
    [JsonProperty("con")]
    public static string HeaderType => "LROE";
    
    [JsonProperty("apa")]
    public static string Section => "1.1";

    [JsonProperty("inte")]
    public IssuerData Issuer { get; set; }

    [JsonProperty("drs")]
    public FiscalData FiscalData { get; set; }
}

public sealed class FiscalData
{
    [JsonProperty("mode")]
    public string Mode => "240";

    [JsonProperty("ejer")]
    public int FiscalYear { get; set; }
}

public sealed class IssuerData
{
    [JsonProperty("nif")]
    public string TaxpayerIdentificationNumber { get; set; }

    [JsonProperty("nrs")]
    public string FirstNameOrCompanyName { get; set; }

    [JsonProperty("ap1")]
    public string Surname { get; set; }

    [JsonProperty("ap2")]
    public string SecondSurname { get; set; }
}
