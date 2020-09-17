using Newtonsoft.Json;
using System.Collections.Generic;

namespace mailinator_csharp_client.Models.Domains.Entities
{
    [JsonArray("domains")]
    public class Domains : List<Domain>
    {
    }
}
