using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace mailinator_csharp_client.Models.Stats.Responses
{
    public class GetTeamInfoResponse
    {
        [JsonProperty("server_time")]
        public DateTime ServerTime { get; set; }
        [JsonProperty("domains")]
        public List<string> Domains { get; set; }
    }
}
