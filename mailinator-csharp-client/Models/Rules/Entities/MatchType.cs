namespace mailinator_csharp_client.Models.Rules.Entities
{
    public enum MatchType
    {
        /// <summary>
        /// Matches if ANY of the conditions are true
        /// </summary>
        ANY,
        /// <summary>
        /// Matches if ALL of the conditions are true
        /// </summary>
        ALL,
        /// <summary>
        /// Always matches
        /// </summary>
        ALWAYS_MATCH
    }
}
