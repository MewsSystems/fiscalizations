using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Mews.Fiscalizations.Basque.Dto.Bizkaia.Header;

public class BizkaiaHeaderData
{
    [JsonProperty("con")]
    public string With { get; } = "LROE";
    
    [JsonProperty("apa")]
    public string Section { get; set; }

    [JsonProperty("inte")]
    public IssuerData Issuer { get; set; }

    [JsonProperty("drs")]
    public FiscalData FiscalData { get; set; }
}

public class FiscalData
{
    [JsonProperty("mode")]
    [JsonConverter(typeof(StringEnumConverter))]
    public Mode Mode { get; set; } 

    [JsonProperty("ejer")]
    public int FiscalYear { get; set; }
}

public class IssuerData
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

public enum Mode
{
    [EnumMember(Value = "140")]
    Item140,
    [EnumMember(Value = "240")]
    Item240
}
