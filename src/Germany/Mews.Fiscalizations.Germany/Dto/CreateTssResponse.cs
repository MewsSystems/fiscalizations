using Newtonsoft.Json;
using System;

namespace Mews.Fiscalizations.Germany.Dto
{
    public sealed class CreateTssResponse : TssResponse
    {
        [JsonProperty("admin_puk")]
        public string AdminPuk {  get; set; }
    }
}
