using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Webhooks.Responses
{
    public class PrivateWebhookResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
