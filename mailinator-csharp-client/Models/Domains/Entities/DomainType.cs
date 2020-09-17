using System.Runtime.Serialization;

namespace mailinator_csharp_client.Models.Domains.Entities
{
    public enum DomainType
    {
        [EnumMember(Value = "private")]
        Private,
        [EnumMember(Value = "public")]
        Public
    }
}
