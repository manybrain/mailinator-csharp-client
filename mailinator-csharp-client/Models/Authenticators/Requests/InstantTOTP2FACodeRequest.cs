using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Authenticators.Requests
{
    public class InstantTOTP2FACodeRequest
    {
        [JsonProperty("totpSecretKey")]
        public string TotpSecretKey;
    }
}
