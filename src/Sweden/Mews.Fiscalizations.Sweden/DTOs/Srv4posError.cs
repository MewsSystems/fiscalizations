using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Sweden.DTOs;

public sealed class Srv4posError
{
    [JsonPropertyName("error")]
    public string Error { get; set; }

    [JsonPropertyName("details")]
    public List<ErrorDetail> Details { get; set; }
}

public sealed class ErrorDetail
{
    [JsonPropertyName("field")]
    public string Field { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }

    [JsonPropertyName("code")]
    public string Code { get; set; }

    [JsonPropertyName("params")]
    public ErrorParams Params { get; set; }
}

public sealed class ErrorParams
{
    [JsonPropertyName("actualValue")]
    public string ActualValue { get; set; }

    [JsonPropertyName("pattern")]
    public string Pattern { get; set; }
}

