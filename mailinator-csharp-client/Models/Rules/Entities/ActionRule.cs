using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace mailinator_csharp_client.Models.Rules.Entities
{
    public class ActionRule
    {
        /// <summary>
        /// Specific action to take if the rule condition was true.
        /// </summary>
        [JsonProperty("action")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ActionType Action;
        /// <summary>
        /// A JsonObject containting action specific data.
        /// </summary>
        [JsonProperty("action_data")]
        public ActionData ActionData;
    }
}
