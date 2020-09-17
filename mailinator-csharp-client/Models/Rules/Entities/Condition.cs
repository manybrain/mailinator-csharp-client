using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace mailinator_csharp_client.Models.Rules.Entities
{
    public class Condition
    {
        /// <summary>
        /// Comparison operation for field and value. Valid Values: EQUALS, PREFIX
        /// </summary>
        [JsonProperty("operation")]
        [JsonConverter(typeof(StringEnumConverter))]
        public OperationType Operation;
        
        [JsonProperty("condition_data")]
        public ConditionData ConditionData;
    }
}
