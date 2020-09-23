using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace mailinator_csharp_client.Models.Stats.Entities
{
    public class Stat
    {
        [JsonProperty("date")]
        public string Date;
        [JsonProperty("retrieved")]
        public Retrieved Retrieved;
        [JsonProperty("sent")]
        public Sent Sent;
    }
}
