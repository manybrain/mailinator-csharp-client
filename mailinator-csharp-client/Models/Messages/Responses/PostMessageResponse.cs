using mailinator_csharp_client.Models.Rules.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace mailinator_csharp_client.Models.Responses
{
    public class PostMessageResponse
    {
        [JsonProperty("status")]
        public string Status;
        [JsonProperty("id")]
        public string Id;
        [JsonProperty("rules_fired")]
        public List<Rule> RulesFired;
    }
}
