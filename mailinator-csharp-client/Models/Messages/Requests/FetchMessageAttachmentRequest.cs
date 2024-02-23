using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Messages.Requests
{
    public class FetchMessageAttachmentRequest
    {
        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("message_id")]
        public string MessageId { get; set; }

        [JsonProperty("attachment_id")]
        public string AttachmentId { get; set; }
    }
}
