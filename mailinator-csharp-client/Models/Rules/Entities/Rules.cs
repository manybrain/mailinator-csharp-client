using Newtonsoft.Json;
using System.Collections.Generic;

namespace mailinator_csharp_client.Models.Rules.Entities
{
    [JsonArray("rules")]
    public class Rules : List<Rule>
    {
    }
}
