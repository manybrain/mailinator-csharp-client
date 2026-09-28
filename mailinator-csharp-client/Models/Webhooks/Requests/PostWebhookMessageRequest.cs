using mailinator_csharp_client.Models.Webhooks.Entities;
using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Webhooks.Requests
{
    public class PostWebhookMessageRequest
    {
        /// <summary>Private domain name, or a webhook token when WebhookToken is omitted.</summary>
        [JsonProperty("domain")]
        public string Domain { get; set; }

        /// <summary>Optional webhook token sent as whtoken when Domain is a domain name.</summary>
        [JsonProperty("whtoken")]
        public string WebhookToken { get; set; }

        /// <summary>Message payload. Set To to the recipient inbox.</summary>
        [JsonIgnore]
        public WebhookMessage Webhook { get; set; }
    }
}
