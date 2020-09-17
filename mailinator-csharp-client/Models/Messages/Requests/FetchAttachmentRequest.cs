using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Messages.Requests
{
    public class FetchAttachmentRequest
    {
        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("inbox")]
        public string Inbox { get; set; }

        [JsonProperty("message_id")]
        public string MessageId { get; set; }

        [JsonProperty("attachment_id")]
        public int AttachmentId { get; set; }
    }
}
