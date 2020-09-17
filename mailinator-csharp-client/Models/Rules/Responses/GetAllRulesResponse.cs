using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Rules.Responses
{
    public class GetAllRulesResponse
    {
        [JsonProperty("rules")]
        public Entities.Rules Rules { get; set; }
    }
}
