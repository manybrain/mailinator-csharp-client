using System.Collections.Generic;
using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Responses
{
    public class GetMessageHeadersResponse
    {
        /// <summary>SMTP headers, including custom headers and multi-valued headers such as received.</summary>
        [JsonProperty("headers")]
        public Dictionary<string, object> Headers;
    }
}
