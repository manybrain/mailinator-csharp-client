using mailinator_csharp_client.Models.Rules.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace mailinator_csharp_client.Models.Domains.Entities
{
    [JsonObject]
    public class Domain
    {
        [JsonProperty("_id")]
        public string Id;
        [JsonProperty("description")]
        public string Description;
        [JsonProperty("enabled")]
        public bool Enabled;
        [JsonProperty("name")]
        public string Name;
        [JsonProperty("rules")]
        public List<Rule> Rules;
    }
}
