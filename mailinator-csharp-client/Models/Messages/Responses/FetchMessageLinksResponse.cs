using Newtonsoft.Json;
using System.Collections.Generic;

namespace mailinator_csharp_client.Models.Responses
{
    public class FetchMessageLinksResponse
    {
        [JsonProperty("links")]
        public List<string> Links;
    }
}
