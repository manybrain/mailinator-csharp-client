using mailinator_csharp_client.Clients.ApiClients.Authenticators;
using mailinator_csharp_client.Clients.ApiClients.Domains;
using mailinator_csharp_client.Clients.ApiClients.Messages;
using mailinator_csharp_client.Clients.ApiClients.Rules;
using mailinator_csharp_client.Clients.ApiClients.Stats;
using mailinator_csharp_client.Clients.ApiClients.Webhooks;
using mailinator_csharp_client.Clients.HttpClient;
using mailinator_csharp_client.Helpers;

namespace mailinator_csharp_client
{
    public class MailinatorClient
    {
        private const string BASE_URI = "https://api.mailinator.com/api/v2";

        public MailinatorClient()
        {
            var httpClient = new HttpClient(BASE_URI);

            InitializeApiClientsWithoutApiToken(httpClient);
        }

        public MailinatorClient(string apiTokenKey) : this()
        {
            if (string.IsNullOrEmpty(apiTokenKey))
                throw new ApiException("Api Token Key should be provided");

            var httpClient = new HttpClient(apiTokenKey, BASE_URI);

            InitializeApiClients(httpClient);
        }

        private void InitializeApiClients(IHttpClient httpClient)
        {
            DomainsClient = new DomainsClient(httpClient, "domains");
            MessagesClient = new MessagesClient(httpClient, "domains");
            RulesClient = new RulesClient(httpClient, "domains");
            StatsClient = new StatsClient(httpClient, string.Empty);
            AuthenticatorsClient = new AuthenticatorsClient(httpClient, string.Empty);
        }

        private void InitializeApiClientsWithoutApiToken(IHttpClient httpClient)
        {
            WebhooksClient = new WebhooksClient(httpClient, "domains");
        }

        public DomainsClient DomainsClient { get; private set; }
        public MessagesClient MessagesClient { get; private set; }
        public RulesClient RulesClient { get; private set; }
        public StatsClient StatsClient { get; private set; }
        public WebhooksClient WebhooksClient { get; private set; }
        public AuthenticatorsClient AuthenticatorsClient { get; private set; }
    }
}
