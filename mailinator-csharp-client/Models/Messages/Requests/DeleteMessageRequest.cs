using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Messages.Requests
{
    public class DeleteMessageRequest
    {
        /// <summary>
        /// private - Delete message from any private domain
        /// [your_private_domain.com] - Delete message from a specific private domain
        /// </summary>
        [JsonProperty("domain")]
        public string Domain { get; set; }

        /// <summary>
        /// Delete message from a specific inbox
        /// </summary>
        [JsonProperty("inbox")]
        public string Inbox { get; set; }

        /// <summary>
        /// Delete message with this ID
        /// </summary>
        [JsonProperty("message_id")]
        public string MessageId { get; set; }
    }
}
