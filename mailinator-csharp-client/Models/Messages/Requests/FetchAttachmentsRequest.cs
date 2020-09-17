using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Messages.Requests
{
    public class FetchAttachmentsRequest
    {
        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("inbox")]
        public string Inbox { get; set; }

        [JsonProperty("message_id")]
        public string MessageId { get; set; }
    }
}
