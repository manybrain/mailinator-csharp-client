using mailinator_csharp_client.Models.Messages.Entities;
using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Messages.Requests
{
    public class FetchInboxRequest
    {
        /// <summary>
        /// public - Fetch Message Summaries from the Public Mailinator System
        /// private - Fetch Message Summaries from all Your Private Domains
        /// [your_private_domain.com] - Fetch Message Summaries from a specific Private Domain
        /// </summary>
        [JsonProperty("domain")]
        public string Domain { get; set; } = "private";

        /// <summary>
        /// null - Fetch All Messages summaries for an entire domain
        /// * - Fetch All Messages summaries for an entire domain
        /// [inbox_name] - Fetch All Messages summaries for a given Inbox
        /// [inbox_name*] - Fetch All Messages summaries for a given Inbox Prefix
        /// </summary>
        [JsonProperty("inbox")]
        public string Inbox { get; set; } = "null";

        /// <summary>
        /// Skip this many emails in your Private Domain. Default Value 0. Required - no
        /// </summary>
        [JsonProperty("skip")]
        public int Skip { get; set; } = 0;

        /// <summary>
        /// Number of emails to fetch from your Private Domain. Default Value 50. Required - no
        /// </summary>
        [JsonProperty("limit")]
        public int Limit { get; set; } = 50;

        /// <summary>
        /// Sort results by ascending or descending. Default Value 'descending'. Required - no
        /// </summary>
        [JsonProperty("sort")]
        public Sort Sort { get; set; } = Sort.desc;

        /// <summary>
        /// true: decode encoded subjects. Default Value 'false'. Required - no
        /// </summary>
        [JsonProperty("decode_subject")]
        public bool DecodeSubject { get; set; } = false;
    }
}
