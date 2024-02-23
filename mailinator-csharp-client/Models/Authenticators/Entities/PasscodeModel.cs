using Newtonsoft.Json;
using System.Collections.Generic;

namespace mailinator_csharp_client.Models.Authenticators.Entities
{
    public class Authenticator
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("time_step")]
        public int TimeStep { get; set; }

        [JsonProperty("futurecodes")]
        public List<string> FutureCodes { get; set; }

        [JsonProperty("next_reset_secs")]
        public int NextResetSeconds { get; set; }

        [JsonProperty("passcode")]
        public string Passcode { get; set; }
    }
}
