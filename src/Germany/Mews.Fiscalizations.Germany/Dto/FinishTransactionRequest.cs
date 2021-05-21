using Newtonsoft.Json;

namespace Mews.Fiscalizations.Germany.Dto
{
    internal sealed class FinishTransactionRequest : TransactionRequest
    {
        [JsonProperty("schema")]
        public Schema Schema { get; set; }
    }
}
