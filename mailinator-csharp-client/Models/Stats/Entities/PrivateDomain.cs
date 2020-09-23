using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Stats.Entities
{
    public class PrivateDomain
    {
        [JsonProperty("pd")]
        public string PD;

        [JsonProperty("enabled")]
        public bool Enabled;
    }
}
