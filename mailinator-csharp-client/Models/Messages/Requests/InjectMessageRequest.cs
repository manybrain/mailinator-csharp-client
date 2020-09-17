using mailinator_csharp_client.Models.Messages.Entities;
using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Messages.Requests
{
    public class InjectMessageRequest
    {
        /// <summary>
        /// private - Inject to any (i.e. first) private domain
        /// [your_private_domain.com] - Inject to specific private domain
        /// </summary>
        [JsonProperty("domain")]
        public string Domain { get; set; }

        /// <summary>
        /// TO destination for injected message
        /// </summary>
        [JsonProperty("inbox")]
        public string Inbox { get; set; }

        [JsonIgnore]
        public MessageToPost Message { get; set; }
    }
}
