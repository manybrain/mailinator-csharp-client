using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Messages.Entities
{
    public class Attachment
    {
        [JsonProperty("filename")]
        public string Filename;
        [JsonProperty("content-disposition")]
        public string ContentDisposition;
        [JsonProperty("content-transfer-encoding")]
        public string ContentTransferEncoding;
        [JsonProperty("content-type")]
        public string ContentType;
        [JsonProperty("attachment-id")]
        public int AttachmentId;
    }
}
