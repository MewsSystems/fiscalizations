using Newtonsoft.Json;
using System;

namespace Mews.Fiscalizations.Germany.Dto
{
    public sealed class GetTransactionRequest
    {
        [JsonProperty("tss_id")]
        public Guid TssId { get; set; }

        [JsonProperty("tx_id")]
        public Guid TransactionId { get; set; }
    }
}
