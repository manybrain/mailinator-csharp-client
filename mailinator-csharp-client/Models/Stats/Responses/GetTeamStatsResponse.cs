using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Stats.Responses
{
    public class GetTeamStatsResponse
    {
        [JsonProperty("stats")]
        public Entities.Stats Stats { get; set; }
    }
}
