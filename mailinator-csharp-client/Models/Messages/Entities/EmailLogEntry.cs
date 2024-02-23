using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Messages.Entities
{
    public class EmailLogEntry
    {
        [JsonProperty("log")]
        public string Log { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("event")]
        public string Event { get; set; }
    }
}
