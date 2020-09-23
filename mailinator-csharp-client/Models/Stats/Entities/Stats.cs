using Newtonsoft.Json;
using System.Collections.Generic;

namespace mailinator_csharp_client.Models.Stats.Entities
{
    [JsonArray("stats")]
    public class Stats : List<Stat>
    {
    }
}
