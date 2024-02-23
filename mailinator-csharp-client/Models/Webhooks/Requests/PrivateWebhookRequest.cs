using mailinator_csharp_client.Models.Webhooks.Entities;
using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Webhooks.Requests
{
    public class PrivateWebhookRequest
    {
        [JsonProperty("wh-token")]
        public string WebhookToken { get; set; }

        [JsonIgnore]
        public Webhook Webhook { get; set; }
    }
}
