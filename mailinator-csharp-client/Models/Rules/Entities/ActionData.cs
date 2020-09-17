using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Rules.Entities
{
    public class ActionData
    {
        [JsonProperty("url")]
        public string Url;
    }
}
