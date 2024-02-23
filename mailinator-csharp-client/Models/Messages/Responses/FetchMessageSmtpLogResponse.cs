using mailinator_csharp_client.Models.Messages.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace mailinator_csharp_client.Models.Responses
{
    public class FetchMessageSmtpLogResponse
    {
        [JsonProperty("log")]
        public List<EmailLogEntry> LogEntries { get; set; }
    }
}
