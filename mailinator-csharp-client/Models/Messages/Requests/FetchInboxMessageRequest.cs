using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Messages.Requests
{
    public class FetchInboxMessageRequest : FetchMessageRequest
    {
        /// <summary>
        /// Fetch Message for this inbox
        /// </summary>
        [JsonProperty("inbox")]
        public string Inbox { get; set; }
    }
}
