using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Authenticators.Requests
{
    public class GetAuthenticatorsByIdRequest
    {
        [JsonProperty("id")]
        public string Id;
    }
}
