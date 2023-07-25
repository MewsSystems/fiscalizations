using Newtonsoft.Json;

namespace Mews.Fiscalizations.Germany.V2.Dto;

internal sealed class FinishTransactionRequest : TransactionRequest
{
    [JsonProperty("schema")]
    public Schema Schema { get; set; }
}