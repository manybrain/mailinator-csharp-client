using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Domains.Requests
{
    public class CreateDomainRequest
    {
        [JsonProperty("name")]
        public string Name;
    }
}
