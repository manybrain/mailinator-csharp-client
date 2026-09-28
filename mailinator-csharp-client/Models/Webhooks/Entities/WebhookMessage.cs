using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace mailinator_csharp_client.Models.Webhooks.Entities
{
    /// <summary>Payload for domain and inbox webhook injection.</summary>
    public class WebhookMessage : Webhook
    {
        [JsonProperty("html")]
        public string Html { get; set; }

        [JsonProperty("headers")]
        public Dictionary<string, string> Headers { get; set; }

        [JsonExtensionData]
        public IDictionary<string, JToken> AdditionalProperties { get; set; }
    }
}
