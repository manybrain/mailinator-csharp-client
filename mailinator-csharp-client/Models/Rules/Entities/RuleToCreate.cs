using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace mailinator_csharp_client.Models.Rules.Entities
{
    public class RuleToCreate
    {
        /// <summary>
        /// 1-255 characters
        /// </summary>
        [JsonProperty("description")]
        public string Description;
        /// <summary>
        /// Rules create enabled are immediately active.
        /// </summary>
        [JsonProperty("enabled")]
        public bool Enabled = true;
        /// <summary>
        /// Indicates condition matching type - must be ANY or ALL
        /// </summary>
        [JsonProperty("match")]
        [JsonConverter(typeof(StringEnumConverter))]
        public MatchType Match = MatchType.ALL;
        /// <summary>
        /// Names must be lowercase and 1-15 characters. They may only contain alphanumeric, dot, and underscore.
        /// </summary>
        [JsonProperty("name")]
        public string Name;
        /// <summary>
        /// An Integer between 1-99999 governing rule execution order. 1 is the highest priority, 99999 is the lowest.
        /// </summary>
        [JsonProperty("priority")]
        public int Priority;
        /// <summary>
        /// Conditions must be an array Conditions objects
        /// </summary>
        [JsonProperty("conditions")]
        public List<Condition> Conditions = new List<Condition>();
        /// <summary>
        /// Actions must be an array of Actions objects
        /// </summary>
        [JsonProperty("actions")]
        public List<ActionRule> Actions = new List<ActionRule>();
    }
}
