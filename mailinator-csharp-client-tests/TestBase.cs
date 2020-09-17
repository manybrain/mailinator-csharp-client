using mailinator_csharp_client;
using mailinator_csharp_client.Models.Domains.Entities;
using mailinator_csharp_client.Models.Messages.Entities;
using mailinator_csharp_client.Models.Messages.Requests;
using mailinator_csharp_client.Models.Rules.Entities;
using mailinator_csharp_client.Models.Rules.Requests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace mailinator_csharp_client_tests
{
    [TestClass]
    public class TestBase
    {
        protected readonly MailinatorClient mailinatorClient;

        private Domain domain;
        private Message message;
        private Rule rule;

        private const string ENV_API_TOKEN = "MAILINATOR_TEST_API_TOKEN";
        private const string ENV_DOMAIN_PRIVATE = "MAILINATOR_TEST_DOMAIN_PRIVATE";
        private const string ENV_INBOX = "MAILINATOR_TEST_INBOX";
        private const string ENV_PHONE_NUMBER = "MAILINATOR_TEST_PHONE_NUMBER";
        private const string ENV_MESSAGE_WITH_ATTACHMENT_ID = "MAILINATOR_TEST_MESSAGE_WITH_ATTACHMENT_ID";
        private const int ENV_ATTACHMENT_ID = 0;
        private const string ENV_DELETE_DOMAIN = "MAILINATOR_TEST_DELETE_DOMAIN";


        protected TestBase()
        {
            PrivateDomain = ENV_DOMAIN_PRIVATE;
            DeleteDomain = ENV_DELETE_DOMAIN;
            PrivateInbox = ENV_INBOX;
            InboxAll = "*";
            MessageIdWithAttachment = ENV_MESSAGE_WITH_ATTACHMENT_ID;
            TeamSMSNumber = ENV_PHONE_NUMBER;
            AttachmentId = ENV_ATTACHMENT_ID;

            mailinatorClient = new MailinatorClient(ENV_API_TOKEN);
        }

        protected Domain Domain
        {
            get
            {
                return domain ?? (domain = GetFirstDomainFromServer());
            }
        }

        protected Message Message
        {
            get
            {
                return message ?? (message= GetFirstMessageFromServer());
            }
        }

        protected Rule Rule
        {
            get
            {
                return rule ?? (rule = GetFirstRuleFromServer());
            }
        }

        protected string PrivateInbox { get; }
        protected string PrivateDomain { get; }
        protected string DeleteDomain { get; }
        protected string InboxAll { get; }
        protected string MessageIdWithAttachment { get; }
        protected string TeamSMSNumber { get; }
        protected int AttachmentId { get; }

        private Domain GetFirstDomainFromServer()
        {
            var allDomainsResponse = mailinatorClient.DomainsClient.GetAllDomainsAsync().Result;

            if (allDomainsResponse == null)
                throw new ArgumentNullException(nameof(allDomainsResponse));

            var domain = allDomainsResponse.Domains.FirstOrDefault();

            if (domain == null)
                throw new ArgumentNullException(nameof(domain));

            return domain;
        }

        private Message GetFirstMessageFromServer()
        {
            var fetchInboxRequest = new FetchInboxRequest() { Domain = Domain.Name, Inbox = InboxAll };
            var fetchInboxResponse = mailinatorClient.MessagesClient.FetchInboxAsync(fetchInboxRequest).Result;

            if (fetchInboxResponse == null)
                throw new ArgumentNullException(nameof(fetchInboxResponse));

            var message = fetchInboxResponse.Messages.FirstOrDefault();

            if (message == null)
                throw new ArgumentNullException(nameof(message));

            return message;
        }

        private Rule GetFirstRuleFromServer()
        {
            var getAllRulesRequest = new GetAllRulesRequest() { DomainId = Domain.Id };
            var getAllRulesResponse = mailinatorClient.RulesClient.GetAllRulesAsync(getAllRulesRequest).Result;

            if (getAllRulesResponse == null)
                throw new ArgumentNullException(nameof(getAllRulesResponse));

            var rule = getAllRulesResponse.Rules.FirstOrDefault();

            if (rule == null)
                throw new ArgumentNullException(nameof(rule));

            return rule;
        }
    }
}
