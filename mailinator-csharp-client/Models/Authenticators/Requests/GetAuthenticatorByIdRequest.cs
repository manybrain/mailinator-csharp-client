using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Authenticators.Requests
{
    public class GetAuthenticatorByIdRequest
    {
        [JsonProperty("id")]
        public string Id;
    }
}
