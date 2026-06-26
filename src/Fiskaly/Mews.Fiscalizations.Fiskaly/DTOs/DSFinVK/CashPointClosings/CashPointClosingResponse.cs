using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings;

internal sealed class CashPointClosingResponse
{
    [JsonPropertyName("closing_id")]
    public Guid Id { get; init; }

    [JsonPropertyName("client_id")]
    public Guid ClientId { get; init; }

    [JsonPropertyName("cash_point_closing_export_id")]
    public long CashPointClosingExportId { get; init; }

    [JsonPropertyName("state")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public CashPointClosingState State { get; init; }

    [JsonPropertyName("error")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public CashPointClosingError Error { get; init; }
}

internal sealed class CashPointClosingError
{
    [JsonPropertyName("code")]
    public string Code { get; init; }

    [JsonPropertyName("message")]
    public string Message { get; init; }
}

internal enum CashPointClosingState
{
    PENDING,
    WORKING,
    COMPLETED,
    ERROR,
    DELETED
}
