using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Stats.Entities
{
    public class SMSNumber
    {
        [JsonProperty("number")]
        public string Number;

        [JsonProperty("country")]
        public string Country;

        [JsonProperty("status")]
        public string Status;
    }
}
