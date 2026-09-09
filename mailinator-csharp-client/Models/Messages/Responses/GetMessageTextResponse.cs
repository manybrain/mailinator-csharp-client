using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Responses
{
    public class GetMessageTextResponse
    {
        /// <summary>Extracted message text, preserving any quoted-printable artifacts returned by the API.</summary>
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
