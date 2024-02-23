using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Webhooks.Entities
{
    public class Webhook
    {
        [JsonProperty("from")]
        public string From;
        [JsonProperty("subject")]
        public string Subject;
        [JsonProperty("text")]
        public string Text;
        [JsonProperty("to")]
        public string To;
    }
}
