using Newtonsoft.Json;
using System.Collections.Generic;

namespace mailinator_csharp_client.Models.Messages.Entities
{
    [JsonArray]
    public class Messages : List<Message>
    {
    }
}
