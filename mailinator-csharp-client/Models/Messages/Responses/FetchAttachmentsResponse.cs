using mailinator_csharp_client.Models.Messages.Entities;
using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Responses
{
    public class FetchAttachmentsResponse
    {
        [JsonProperty("attachments")]
        public Attachments Attachments;
    }
}
