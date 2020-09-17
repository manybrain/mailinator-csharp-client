using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Rules.Entities
{
    public class ResponseStatus
    {
        [JsonProperty("status")]
        public string Status;
    }
}
