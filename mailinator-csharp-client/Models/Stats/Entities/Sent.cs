using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Stats.Entities
{
    public class Sent
    {
        [JsonProperty("sms")]
        public int SMS;

        [JsonProperty("email")]
        public int Email;

    }
}
