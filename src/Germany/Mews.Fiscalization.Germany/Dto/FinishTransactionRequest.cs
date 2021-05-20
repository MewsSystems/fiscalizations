using Newtonsoft.Json;

namespace Mews.Fiscalization.Germany.Dto
{
    internal sealed class FinishTransactionRequest : TransactionRequest
    {
        [JsonProperty("schema")]
        public Schema Schema { get; set; }
    }
}
