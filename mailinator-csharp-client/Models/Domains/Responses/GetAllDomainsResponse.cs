using mailinator_csharp_client.Models.Domains.Entities;
using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Domains.Responses
{
    public class GetAllDomainsResponse
    {
        [JsonProperty("domains")]
        public Entities.Domains Domains { get; set; }
    }
}
