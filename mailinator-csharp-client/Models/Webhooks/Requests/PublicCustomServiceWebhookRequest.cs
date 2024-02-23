using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Webhooks.Requests
{
    public class PublicCustomServiceWebhookRequest : PublicWebhookRequest
    {
        [JsonProperty("customService")]
        public string CustomService { get; set; }
    }
}
