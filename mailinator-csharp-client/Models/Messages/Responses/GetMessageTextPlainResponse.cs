using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Responses
{
    public class GetMessageTextPlainResponse
    {
        /// <summary>The text/plain message body.</summary>
        [JsonProperty("text/plain")]
        public string TextPlain { get; set; }
    }
}
