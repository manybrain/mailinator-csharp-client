using mailinator_csharp_client.Models.Messages.Entities;
using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Responses
{
    public class GetMessageSummaryResponse
    {
        /// <summary>Message metadata. Body and attachment content are not returned by this endpoint.</summary>
        [JsonProperty("summary")]
        public Message Summary;
    }
}
