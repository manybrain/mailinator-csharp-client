using System.Runtime.Serialization;

namespace mailinator_csharp_client.Models.Messages.Entities
{
    public enum Sort
    {
        [EnumMember(Value = "ascending")]
        asc,
        [EnumMember(Value = "descending")]
        desc
    }
}
