namespace mailinator_csharp_client.Models.Rules.Entities
{
    public enum ActionType
    {
        /// <summary>
        /// POST JSON version of message to HTTP Rest Endpoint. Action Data: url : your HTTP Rest Endpoint url
        /// </summary>
        WEBHOOK,
        /// <summary>
        /// Drop this email. No further rules will execute.
        /// </summary>
        DROP
    }
}
