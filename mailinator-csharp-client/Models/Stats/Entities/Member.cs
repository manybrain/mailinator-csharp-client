using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Stats.Entities
{
    public class Member
    {
        [JsonProperty("role")]
        public string Role;

        [JsonProperty("_id")]
        public string Id;

        [JsonProperty("email")]
        public string Email;
    }
}
