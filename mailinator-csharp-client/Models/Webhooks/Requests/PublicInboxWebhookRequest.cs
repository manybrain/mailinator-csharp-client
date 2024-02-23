using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Webhooks.Requests
{
    public class PublicInboxWebhookRequest : PublicWebhookRequest
    {
        [JsonProperty("inbox")]
        public string Inbox { get; set; }
    }
}
