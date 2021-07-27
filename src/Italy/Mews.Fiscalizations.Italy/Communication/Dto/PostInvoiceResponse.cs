using Newtonsoft.Json;

namespace Mews.Fiscalizations.Italy.Communication
{
    internal class PostInvoiceResponse
    {
        [JsonProperty("fid")]
        public string FileId { get; set; }

        [JsonProperty("msg")]
        public string Message { get; set; }
    }
}