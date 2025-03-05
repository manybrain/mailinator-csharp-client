using mailinator_csharp_client.Models.Messages.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace mailinator_csharp_client.Models.Responses
{
    public class FetchMessageLinksFullResponse
    {
        [JsonProperty("links")]
        public List<LinkEntity> Links;
    }
}
