using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Messages.Requests
{
    public class DeleteAllDomainMessagesRequest
    {
        /// <summary>
        /// private - Delete ALL messages in all your private domains
        /// [your_private_domain.com] - Delete all messages in a specific private domain
        /// </summary>
        [JsonProperty("domain")]
        public string Domain { get; set; }

        /// <summary>
        /// null - Delete from all inboxes, or
        /// * - Delete from all inboxes
        /// </summary>
        [JsonProperty("inbox")]
        public string Inbox { get; set; }
    }
}
