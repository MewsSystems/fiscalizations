

using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Basque.Dto.Bizkaia.Header;

public sealed class BizkaiaHeaderData
{
    [JsonPropertyName("con")]
    public string HeaderType => "LROE";
    
    [JsonPropertyName("apa")]
    public string Section => "1.1";

    [JsonPropertyName("inte")]
    public IssuerData Issuer { get; set; }

    [JsonPropertyName("drs")]
    public FiscalData FiscalData { get; set; }
}

public sealed class FiscalData
{
    [JsonPropertyName("mode")]
    public Mode Mode { get; set; } 

    [JsonPropertyName("ejer")]
    public int FiscalYear { get; set; }
}

public sealed class IssuerData
{
    [JsonPropertyName("nif")]
    public string TaxpayerIdentificationNumber { get; set; }

    [JsonPropertyName("nrs")]
    public string FirstNameOrCompanyName { get; set; }

    [JsonPropertyName("ap1")]
    public string Surname { get; set; }

    [JsonPropertyName("ap2")]
    public string SecondSurname { get; set; }
}

public enum Mode
{
    Item140 = 140,
    Item240 = 240
}
