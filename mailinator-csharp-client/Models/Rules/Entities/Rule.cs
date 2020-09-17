using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace mailinator_csharp_client.Models.Rules.Entities
{
    public class Rule
    {
        /// <summary>
        /// System generated, unique Rule Id. You may use this ID to query a specific rule
        /// </summary>
        [JsonProperty("_id")]
        public string Id;
        /// <summary>
        /// Names must be lowercase and 1-20 characters. They may only contain alphanumeric, dot, and underscore.
        /// </summary>
        [JsonProperty("name")]
        public string Name;
        /// <summary>
        /// 1-255 characters
        /// </summary>
        [JsonProperty("description")]
        public string Description;
        /// <summary>
        /// Enabled rules are immediately active.
        /// </summary>
        [JsonProperty("enabled")]
        public bool Enabled;
        /// <summary>
        /// Indicates condition matching type - must be ANY, ALL, or ALWAYS_MATCH
        /// </summary>
        [JsonProperty("match_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public MatchType Match;
        /// <summary>
        /// An Integer between 1-99999 governing rule execution order. 1 is the highest priority, 99999 is the lowest.
        /// </summary>
        [JsonProperty("priority")]
        public int Priority;
        /// <summary>
        /// Conditions must be an array Conditions objects.
        /// </summary>
        [JsonProperty("conditions")]
        public List<Condition> Conditions;
        /// <summary>
        /// Actions must be an array of Actions objects.
        /// </summary>
        [JsonProperty("actions")]
        public List<ActionRule> Actions;
    }
}
