using mailinator_csharp_client.Models.Authenticators.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace mailinator_csharp_client.Models.Authenticators.Responses
{
    public class GetAuthenticatorResponse
    {
        [JsonProperty("passcodes")]
        public List<Authenticator> Passcodes { get; set; }
    }
}
