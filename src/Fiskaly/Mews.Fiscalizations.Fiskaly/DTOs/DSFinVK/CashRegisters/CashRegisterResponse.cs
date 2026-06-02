using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashRegisters;

internal sealed class CashRegisterResponse
{
    [JsonPropertyName("client_id")]
    public Guid ClientId { get; init; }

    [JsonPropertyName("cash_register_type")]
    public string CashRegisterType { get; init; }

    [JsonPropertyName("tss_id")]
    public Guid TssId { get; init; }

    [JsonPropertyName("serial_number")]
    public string SerialNumber { get; init; }

    [JsonPropertyName("brand")]
    public string Brand { get; init; }

    [JsonPropertyName("model")]
    public string Model { get; init; }

    [JsonPropertyName("software")]
    public CashRegisterSoftware Software { get; init; }

    [JsonPropertyName("base_currency_code")]
    public string BaseCurrencyCode { get; init; }
}
