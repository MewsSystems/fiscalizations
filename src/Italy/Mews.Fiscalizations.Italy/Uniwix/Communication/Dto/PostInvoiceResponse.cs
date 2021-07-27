using Newtonsoft.Json;

namespace Mews.Fiscalizations.Italy.Uniwix.Communication.Dto
{
    internal class PostInvoiceResponse
    {
        [JsonProperty("fid")]
        public string FileId { get; set; }

        [JsonProperty("msg")]
        public string Message { get; set; }
    }
}