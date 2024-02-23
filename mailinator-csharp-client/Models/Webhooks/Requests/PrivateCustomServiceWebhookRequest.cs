using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Webhooks.Requests
{
    public class PrivateCustomServiceWebhookRequest : PrivateWebhookRequest
    {
        [JsonProperty("customService")]
        public string CustomService { get; set; }
    }
}
