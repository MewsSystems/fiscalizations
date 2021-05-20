using Newtonsoft.Json;

namespace Mews.Fiscalization.Uniwix.Communication
{
    internal class PostInvoiceResponse
    {
        [JsonProperty("fid")]
        public string FileId { get; set; }

        [JsonProperty("msg")]
        public string Message { get; set; }
    }
}