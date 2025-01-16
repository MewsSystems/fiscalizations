using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Sweden.DTOs;

internal class SendDataResponse
{
    [JsonPropertyName("response")]
    public string Response { get; set; }

    [JsonPropertyName("controlUnitSerial")]
    public string ControlUnitSerial { get; set; }
}