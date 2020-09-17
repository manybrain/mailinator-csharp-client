using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Rules.Entities
{
    public class ConditionData
    {
        /// <summary>
        /// The message field to compare. Valid Values: to
        /// </summary>
        [JsonProperty("field")]
        public string Field;
        /// <summary>
        /// The value to compare. Valid Values: Any - E.g., "joe", "bob"
        /// </summary>
        [JsonProperty("value")]
        public string Value;
    }
}
