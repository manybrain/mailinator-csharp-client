using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Stats.Entities
{
    public class Retrieved
    {
        [JsonProperty("web_public")]
        public int WebPublic;

        [JsonProperty("api_error")]
        public int ApiError;

        [JsonProperty("web_private")]
        public int WebPrivate;

        [JsonProperty("api_email")]
        public int ApiEmail;

    }
}
