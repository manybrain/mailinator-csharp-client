using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Messages.Requests
{
    public class FetchInboxMessageLinksRequest : FetchMessageLinksRequest
    {
        [JsonProperty("inbox")]
        public string Inbox { get; set; }
    }
}
