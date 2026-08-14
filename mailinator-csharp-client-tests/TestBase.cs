using mailinator_csharp_client;
using mailinator_csharp_client.Models.Domains.Entities;
using mailinator_csharp_client.Models.Domains.Requests;
using mailinator_csharp_client.Models.Domains.Responses;
using mailinator_csharp_client.Models.Messages.Entities;
using mailinator_csharp_client.Models.Messages.Requests;
using mailinator_csharp_client.Models.Responses;
using mailinator_csharp_client.Models.Rules.Entities;
using mailinator_csharp_client.Models.Rules.Requests;
using mailinator_csharp_client.Models.Rules.Responses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace mailinator_csharp_client_tests
{
    [TestClass]
    public class TestBase
    {
        protected MailinatorClient mailinatorClient;

        private Domain domain;

        private const string ENV_API_TOKEN = "MAILINATOR_TEST_API_TOKEN";
        private const string ENV_DOMAIN_PRIVATE = "MAILINATOR_TEST_DOMAIN_PRIVATE";
        private const string ENV_INBOX = "MAILINATOR_TEST_INBOX";
        private const string ENV_PHONE_NUMBER = "MAILINATOR_TEST_PHONE_NUMBER";
        private const string ENV_MESSAGE_WITH_ATTACHMENT_ID = "MAILINATOR_TEST_MESSAGE_WITH_ATTACHMENT_ID";
        private const string ENV_ATTACHMENT_ID = "MAILINATOR_TEST_ATTACHMENT_ID";
        private const string ENV_DELETE_DOMAIN = "MAILINATOR_TEST_DELETE_DOMAIN";
        private const string ENV_WEBHOOKTOKEN_PRIVATEDOMAIN = "MAILINATOR_TEST_WEBHOOKTOKEN_PRIVATEDOMAIN";
        private const string ENV_WEBHOOKTOKEN_CUSTOMSERVICE = "MAILINATOR_TEST_WEBHOOKTOKEN_CUSTOMSERVICE";
        private const string ENV_AUTH_SECRET = "MAILINATOR_TEST_AUTH_SECRET";
        private const string ENV_AUTH_ID = "MAILINATOR_TEST_AUTH_ID";
        private const string ENV_WEBHOOK_INBOX = "MAILINATOR_TEST_WEBHOOK_INBOX";
        private const string ENV_WEBHOOK_CUSTOMSERVICE = "MAILINATOR_TEST_WEBHOOK_CUSTOMSERVICE";

        private static readonly string ApiToken;

        static TestBase()
        {
            LoadDotEnv();
            ApiToken = GetEnvironmentVariable(ENV_API_TOKEN);
        }

        protected TestBase()
        {
            PrivateDomain = GetEnvironmentVariable(ENV_DOMAIN_PRIVATE);
            DeleteDomain = GetEnvironmentVariable(ENV_DELETE_DOMAIN);
            PrivateInbox = GetEnvironmentVariable(ENV_INBOX);
            InboxAll = "*";
            MessageIdWithAttachment = GetEnvironmentVariable(ENV_MESSAGE_WITH_ATTACHMENT_ID);
            TeamSMSNumber = GetEnvironmentVariable(ENV_PHONE_NUMBER);
            AttachmentId = GetEnvironmentVariable(ENV_ATTACHMENT_ID);
            WebhookTokenPrivateDomain = GetEnvironmentVariable(ENV_WEBHOOKTOKEN_PRIVATEDOMAIN);
            WebhookTokenCustomService = GetEnvironmentVariable(ENV_WEBHOOKTOKEN_CUSTOMSERVICE);
            AuthSecret = GetEnvironmentVariable(ENV_AUTH_SECRET);
            AuthId = GetEnvironmentVariable(ENV_AUTH_ID);
            WebhookInbox = GetEnvironmentVariable(ENV_WEBHOOK_INBOX);
            WebhookCustomService = GetEnvironmentVariable(ENV_WEBHOOK_CUSTOMSERVICE);

        }

        [TestInitialize]
        public void RequireApiToken()
        {
            if (string.IsNullOrWhiteSpace(ApiToken))
                Assert.Inconclusive($"Skipping integration test: set {ENV_API_TOKEN} in the repository .env file or process environment.");

            mailinatorClient = new MailinatorClient(ApiToken);
        }

        protected Domain Domain
        {
            get
            {
                return domain ?? (domain = GetFirstDomainFromServer());
            }
        }

        protected string PrivateInbox { get; }
        protected string PrivateDomain { get; }
        protected string DeleteDomain { get; }
        protected string InboxAll { get; }
        protected string MessageIdWithAttachment { get; }
        protected string TeamSMSNumber { get; }
        protected string AttachmentId { get; }
        protected string WebhookTokenPrivateDomain { get; }
        protected string WebhookTokenCustomService { get; }
        protected string AuthSecret { get; }
        protected string AuthId { get; }
        protected string WebhookInbox { get; }
        protected string WebhookCustomService { get; }

        private Domain GetFirstDomainFromServer()
        {
            var allDomainsResponse = mailinatorClient.DomainsClient.GetAllDomainsAsync().Result;
            return GetFirstItemFromResponse(allDomainsResponse?.Domains.ToArray(), nameof(allDomainsResponse));
        }

        private T GetFirstItemFromResponse<T>(T[] items, string responseName)
        {
            if (items == null || !items.Any())
                throw new ArgumentNullException(responseName);

            return items.FirstOrDefault();
        }

        public async Task<Tuple<CreateDomainResponse, string>> CreateNewDomainAsync()
        {
            var name = $"test{DateTime.UtcNow.Ticks}.testinator.com";
            var request = new CreateDomainRequest() { Name = name };
            var response = await mailinatorClient.DomainsClient.CreateDomainAsync(request);

            return new Tuple<CreateDomainResponse, string>(response, name);
        }

        public Task<CreateRuleResponse> CreateNewRuleAsync()
        {
            var rule = new RuleToCreate()
            {
                Name = DateTime.UtcNow.Ticks.ToString(),
                Priority = 15,
                Description = "Description",
                Conditions = new List<Condition>()
                {
                    new Condition()
                    {
                        Operation = OperationType.PREFIX,
                        ConditionData = new ConditionData()
                        {
                            Field = "to",
                            Value = "raul"
                        }
                    }
                },
                Enabled = true,
                Match = MatchType.ANY,
                Actions = new List<ActionRule>() { new ActionRule() { Action = ActionType.WEBHOOK,
                                                                      ActionData = new ActionData() { Url = "https://google.com" } } }
            };
            var request = new CreateRuleRequest() { DomainId = Domain.Name, Rule = rule };
            return mailinatorClient.RulesClient.CreateRuleAsync(request);
        }

        public Task<PostMessageResponse> PostNewMessageAsync(string domain, string inbox)
        {
            var message = new MessageToPost()
            {
                Subject = "Testing message",
                From = $"testPostMessageRequest {DateTime.UtcNow.Ticks}",
                Text = $"text {DateTime.UtcNow.Ticks}"
            };
            var request = new PostMessageRequest() { Domain = domain, Inbox = inbox, Message = message };
            return mailinatorClient.MessagesClient.PostMessageAsync(request);
        }

        private static string GetEnvironmentVariable(string name)
        {
            return Environment.GetEnvironmentVariable(name);
        }

        private static void LoadDotEnv()
        {
            var dotEnvPath = FindDotEnv(Environment.CurrentDirectory) ?? FindDotEnv(AppDomain.CurrentDomain.BaseDirectory);
            if (dotEnvPath == null)
                return;

            foreach (var line in File.ReadAllLines(dotEnvPath))
            {
                var trimmedLine = line.Trim();
                if (trimmedLine.Length == 0 || trimmedLine.StartsWith("#"))
                    continue;

                if (trimmedLine.StartsWith("export "))
                    trimmedLine = trimmedLine.Substring("export ".Length).TrimStart();

                var separatorIndex = trimmedLine.IndexOf('=');
                if (separatorIndex <= 0)
                    continue;

                var name = trimmedLine.Substring(0, separatorIndex).Trim();
                var value = trimmedLine.Substring(separatorIndex + 1).Trim();
                if (value.Length >= 2 && ((value.StartsWith("\"") && value.EndsWith("\"")) || (value.StartsWith("'") && value.EndsWith("'"))))
                    value = value.Substring(1, value.Length - 2);

                if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable(name)))
                    Environment.SetEnvironmentVariable(name, value);
            }
        }

        private static string FindDotEnv(string startDirectory)
        {
            if (string.IsNullOrWhiteSpace(startDirectory))
                return null;

            var directory = new DirectoryInfo(startDirectory);
            while (directory != null)
            {
                var path = Path.Combine(directory.FullName, ".env");
                if (File.Exists(path))
                    return path;

                directory = directory.Parent;
            }

            return null;
        }
    }
}
