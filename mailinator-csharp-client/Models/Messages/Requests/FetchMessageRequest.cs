using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Messages.Requests
{
    public class FetchMessageRequest
    {
        /// <summary>
        /// public - Fetch Message Summaries from the Public Mailinator System
        /// private - Fetch Message Summaries from all Your Private Domains
        /// [your_private_domain.com] - Fetch Message Summaries from a specific Private Domain
        /// </summary>
        [JsonProperty("domain")]
        public string Domain { get; set; }

        /// <summary>
        /// Fetch Message for this inbox
        /// </summary>
        [JsonProperty("inbox")]
        public string Inbox { get; set; }

        /// <summary>
        /// Fetch Message with this ID (found via previous Message Summary call)
        /// </summary>
        [JsonProperty("message_id")]
        public string MessageId { get; set; }
    }
}
