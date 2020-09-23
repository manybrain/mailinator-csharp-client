using mailinator_csharp_client.Models.Stats.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace mailinator_csharp_client.Models.Stats.Responses
{
    public class GetTeamResponse
    {
        [JsonProperty("private_domains")]
        public List<PrivateDomain> PrivateDomains { get; set; }
        [JsonProperty("sms_numbers")]
        public List<SMSNumber> SMSNumbers { get; set; }
        [JsonProperty("members")]
        public List<Member> Members { get; set; }
        [JsonProperty("plan_data")]
        public PlanData PlanData { get; set; }
        [JsonProperty("_id")]
        public string Id { get; set; }
        [JsonProperty("plan")]
        public string Plan { get; set; }
        [JsonProperty("team_name")]
        public string TeamName { get; set; }
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
