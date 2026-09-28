using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Messages.Requests
{
    public class GetMessageTextRequest
    {
        /// <summary>The domain containing the message.</summary>
        [JsonProperty("domain")]
        public string Domain { get; set; }

        /// <summary>The message ID returned by a message listing.</summary>
        [JsonProperty("message_id")]
        public string MessageId { get; set; }
    }
}
