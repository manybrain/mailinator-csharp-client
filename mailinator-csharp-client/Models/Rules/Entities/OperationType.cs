using System.Runtime.Serialization;

namespace mailinator_csharp_client.Models.Rules.Entities
{
    public enum OperationType
    {
        /// <summary>
        /// Matches when the field (e.g. "to") exactly matches an inbox (e.g. "joe")
        /// </summary>
        [EnumMember(Value = "EQUALS")]
        EQUALS,
        /// <summary>
        /// Matches when the field (e.g. "to") starts with a string (e.g. "test" matches "test", "test1", "test9999")
        /// </summary>
        [EnumMember(Value = "PREFIX")]
        PREFIX,

        [EnumMember(Value = "STARTS_WITH")]
        STARTS_WITH
    }
}
