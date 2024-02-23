using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Messages.Requests
{
    public class FetchLatestMessagesRequest
    {
        [JsonProperty("domain")]
        public string Domain { get; set; }
    }
}
