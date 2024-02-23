using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Messages.Requests
{
    public class FetchMessageRawRequest
    {
        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("message_id")]
        public string MessageId { get; set; }
    }
}
