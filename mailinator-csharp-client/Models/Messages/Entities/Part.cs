using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace mailinator_csharp_client.Models.Messages.Entities
{
    public class Part
    {
        [JsonProperty("headers")]
        public Dictionary<String, Object> Headers;
        [JsonProperty("body")]
        public string Body;
    }
}
