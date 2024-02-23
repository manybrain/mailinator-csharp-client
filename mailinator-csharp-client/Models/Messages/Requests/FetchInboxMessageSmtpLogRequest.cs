using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Messages.Requests
{
    public class FetchInboxMessageSmtpLogRequest : FetchMessageSmtpLogRequest
    {
        [JsonProperty("inbox")]
        public string Inbox { get; set; }
    }
}
