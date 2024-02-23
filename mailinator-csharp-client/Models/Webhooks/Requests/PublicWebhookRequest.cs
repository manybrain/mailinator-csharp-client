using mailinator_csharp_client.Models.Webhooks.Entities;
using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Webhooks.Requests
{
    public class PublicWebhookRequest
    {
        [JsonIgnore]
        public Webhook Webhook { get; set; }
    }
}
