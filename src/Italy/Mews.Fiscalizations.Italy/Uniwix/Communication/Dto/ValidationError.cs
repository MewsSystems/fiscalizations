using System.Collections.Generic;
using Newtonsoft.Json;

namespace Mews.Fiscalizations.Italy.Uniwix.Communication.Dto
{
    internal class ValidationError
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("errors")]
        public IEnumerable<string> Errors { get; set; }
    }
}