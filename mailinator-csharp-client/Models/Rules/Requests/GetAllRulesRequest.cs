using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Rules.Requests
{
    public class GetAllRulesRequest
    {
        /// <summary>
        /// This must be the Domain name or the Domain id
        /// </summary>
        [JsonProperty("domain_id")]
        public string DomainId { get; set; }
    }
}
