using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Messages.Entities
{
    public class MessageToPost
    {
        [JsonProperty("subject")]
        public string Subject;
        [JsonProperty("from")]
        public string From;
        [JsonProperty("text")]
        public string Text;
    }
}
