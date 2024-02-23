using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Messages.Requests
{
    public class FetchInboxMessageRawRequest : FetchMessageRawRequest
    {
        [JsonProperty("inbox")]
        public string Inbox { get; set; }
    }
}
