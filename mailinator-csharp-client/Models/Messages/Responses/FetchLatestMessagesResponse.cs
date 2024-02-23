
using mailinator_csharp_client.Models.Messages.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace mailinator_csharp_client.Models.Responses
{
    public class FetchLatestMessagesResponse
    {
        //[JsonProperty("domain")]
        //public string Domain;
        [JsonProperty("to")]
        public string To;
        [JsonProperty("msgs")]
        public List<Message> Messages;
    }
}
