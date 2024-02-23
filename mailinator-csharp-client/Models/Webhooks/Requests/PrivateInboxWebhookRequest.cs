using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Webhooks.Requests
{
    public class PrivateInboxWebhookRequest : PrivateWebhookRequest
    {
        [JsonProperty("inbox")]
        public string Inbox { get; set; }
    }
}
