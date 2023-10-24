using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Basque.Dto.Bizkaia.Header;

public sealed class BizkaiaHeaderData
{
    [JsonPropertyName("con")]
    public static string HeaderType => "LROE";
    
    [JsonPropertyName("apa")]
    public static string Section => "1.1";

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
    [EnumMember(Value = "140")]
    Item140,
    [EnumMember(Value = "240")]
    Item240
}
