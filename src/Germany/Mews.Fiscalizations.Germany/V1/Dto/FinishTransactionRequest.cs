using Newtonsoft.Json;

namespace Mews.Fiscalizations.Germany.V1.Dto
{
    internal sealed class FinishTransactionRequest : TransactionRequest
    {
        [JsonProperty("schema")]
        public Schema Schema { get; set; }
    }
}
