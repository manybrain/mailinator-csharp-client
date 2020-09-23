using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Stats.Entities
{
    public class PlanData
    {
        [JsonProperty("storage_mb")]
        public int StorageMb;

        [JsonProperty("num_private_domains")]
        public int NumberOfPrivateDomains;

        [JsonProperty("email_reads_per_day")]
        public int EmailReadsPerDay;

        [JsonProperty("team_accounts")]
        public int TeamAccounts;
    }
}
