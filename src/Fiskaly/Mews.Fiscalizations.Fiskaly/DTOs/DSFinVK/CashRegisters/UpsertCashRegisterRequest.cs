using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashRegisters;

internal sealed class UpsertCashRegisterRequest
{
    [JsonPropertyName("cash_register_type")]
    public CashRegisterTypeRequest CashRegisterType { get; init; }

    [JsonPropertyName("brand")]
    public string Brand { get; init; }

    [JsonPropertyName("model")]
    public string Model { get; init; }

    [JsonPropertyName("software")]
    public CashRegisterSoftware Software { get; init; }

    [JsonPropertyName("base_currency_code")]
    public string BaseCurrencyCode { get; init; }
}

internal sealed class CashRegisterTypeRequest
{
    [JsonPropertyName("type")]
    public string Type { get; init; }

    [JsonPropertyName("tss_id")]
    public Guid TssId { get; init; }
}

internal sealed class CashRegisterSoftware
{
    [JsonPropertyName("brand")]
    public string Brand { get; init; }

    [JsonPropertyName("version")]
    public string Version { get; init; }
}
