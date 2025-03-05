using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Messages.Entities
{
    public class LinkEntity
    {
        [JsonProperty("link")]
        public string Link;
        [JsonProperty("text")]
        public string Text;
    }
}
