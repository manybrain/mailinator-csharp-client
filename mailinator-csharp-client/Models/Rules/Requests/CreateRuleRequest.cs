using mailinator_csharp_client.Models.Rules.Entities;
using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Rules.Requests
{
    public class CreateRuleRequest
    {
        /// <summary>
        /// This must be the Domain name or the Domain id (i.e. your private domain)
        /// </summary>
        [JsonProperty("domain_id")]
        public string DomainId { get; set; }

        public RuleToCreate Rule { get; set; } = new RuleToCreate();
    }
}
