using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Responses
{
    public class GetMessageTextHtmlResponse
    {
        /// <summary>The text/html message body, preserving HTML markup.</summary>
        [JsonProperty("text/html")]
        public string TextHtml { get; set; }
    }
}
