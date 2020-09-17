using mailinator_csharp_client.Clients.ApiClients.Domains;
using mailinator_csharp_client.Clients.ApiClients.Messages;
using mailinator_csharp_client.Clients.ApiClients.Rules;
using mailinator_csharp_client.Clients.HttpClient;
using mailinator_csharp_client.Helpers;

namespace mailinator_csharp_client
{
    public class MailinatorClient
    {
        private const string BASE_URI = "https://mailinator.com/api/v2/domains";
        private readonly IHttpClient httpClient;

        public MailinatorClient(string apiTokenKey)
        {
            if (string.IsNullOrEmpty(apiTokenKey))
                throw new ApiException("Api Token Key should be provided");

            httpClient = new HttpClient(apiTokenKey, BASE_URI);

            InitializeApiClients();
        }

        private void InitializeApiClients()
        {
            DomainsClient = new DomainsClient(httpClient);
            MessagesClient = new MessagesClient(httpClient);
            RulesClient = new RulesClient(httpClient);
        }

        public DomainsClient DomainsClient { get; private set; }
        public MessagesClient MessagesClient { get; private set; }
        public RulesClient RulesClient { get; private set; }
    }
}
