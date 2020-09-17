using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Messages.Requests
{
    public class FetchSMSMessagesRequest
    {
        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("YOUR_TEAM_SMS_NUMBER")]
        public string TeamSMSNumber { get; set; }
    }
}
