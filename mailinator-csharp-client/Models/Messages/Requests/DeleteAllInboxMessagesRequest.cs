using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Messages.Requests
{
    public class DeleteAllInboxMessagesRequest
    {
        /// <summary>
        /// private - Delete all messages from an inbox from any private domain
        /// [your_private_domain.com] - Delete all messages from an inbox from a specific private domain
        /// </summary>
        [JsonProperty("domain")]
        public string Domain { get; set; }

        /// <summary>
        /// Delete all messages from a specific inbox
        /// </summary>
        [JsonProperty("inbox")]
        public string Inbox { get; set; }
    }
}
