﻿using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Domains.Requests
{
    public class DeleteDomainRequest
    {
        /// <summary>
        /// This must be the Domain name or the Domain id
        /// </summary>
        [JsonProperty("domain_id")]
        public string DomainId { get; set; }
    }
}
