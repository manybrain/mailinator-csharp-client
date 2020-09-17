using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Messages.Entities
{
    public class DeletedMessages
    {
        [JsonProperty("status")]
        public string Status;
        [JsonProperty("count")]
        public int Count;
    }
}
