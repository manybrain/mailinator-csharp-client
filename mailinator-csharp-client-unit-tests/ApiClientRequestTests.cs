using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mailinator_csharp_client.Clients.ApiClients.Authenticators;
using mailinator_csharp_client.Clients.ApiClients.Domains;
using mailinator_csharp_client.Clients.ApiClients.Messages;
using mailinator_csharp_client.Clients.ApiClients.Rules;
using mailinator_csharp_client.Clients.ApiClients.Stats;
using mailinator_csharp_client.Clients.ApiClients.Webhooks;
using mailinator_csharp_client.Clients.HttpClient;
using mailinator_csharp_client.Models.Authenticators.Requests;
using mailinator_csharp_client.Models.Domains.Requests;
using mailinator_csharp_client.Models.Domains.Responses;
using mailinator_csharp_client.Models.Messages.Entities;
using mailinator_csharp_client.Models.Messages.Requests;
using mailinator_csharp_client.Models.Rules.Requests;
using mailinator_csharp_client.Models.Webhooks.Entities;
using mailinator_csharp_client.Models.Webhooks.Requests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace mailinator_csharp_client_unit_tests
{
    [TestClass]
    public class ApiClientRequestTests
    {
        [TestMethod]
        public async Task ListDomainMessagesAsync_BuildsDomainRouteAndAllQueryParameters()
        {
            var httpClient = new RecordingHttpClient();
            var client = new MessagesClient(httpClient, "domains");

            await client.ListDomainMessagesAsync(new ListDomainMessagesRequest
            {
                Domain = "example.com", Inbox = "orders*", Skip = 10, Limit = 20,
                Sort = Sort.asc, DecodeSubject = true, Cursor = "next+page=",
                Full = true, Delete = "30s", Wait = "10s"
            });

            AssertRequest(httpClient, Method.Get, "domains/{domain}/inboxes",
                "domain", "example.com", "inbox", "orders*", "skip", "10", "limit", "20",
                "sort", "asc", "decode_subject", "True", "cursor", "next+page=",
                "full", "True", "delete", "30s", "wait", "10s");
            Assert.AreEqual(9, httpClient.Request.Parameters.Count(p => p.Type == ParameterType.QueryString));
            Assert.AreEqual(ParameterType.QueryString, httpClient.Request.Parameters.Single(p => p.Name == "inbox").Type);
            Assert.AreEqual(ParameterType.UrlSegment, httpClient.Request.Parameters.Single(p => p.Name == "domain").Type);
        }

        [TestMethod]
        public async Task ListDomainMessagesAsync_DefaultsToEntireDomainWithoutOptionalFilters()
        {
            var httpClient = new RecordingHttpClient();
            var client = new MessagesClient(httpClient, "domains");

            await client.ListDomainMessagesAsync(new ListDomainMessagesRequest());

            AssertRequest(httpClient, Method.Get, "domains/{domain}/inboxes",
                "domain", "private", "skip", "0", "limit", "50", "sort", "desc", "decode_subject", "False");
            CollectionAssert.AreEquivalent(new[] { "skip", "limit", "sort", "decode_subject" },
                httpClient.Request.Parameters.Where(p => p.Type == ParameterType.QueryString).Select(p => p.Name).ToArray());
        }

        [TestMethod]
        public async Task ListDomainMessagesAsync_PreservesWildcardAndExplicitFalse()
        {
            var httpClient = new RecordingHttpClient();
            var client = new MessagesClient(httpClient, "domains");

            await client.ListDomainMessagesAsync(new ListDomainMessagesRequest { Inbox = "*", Full = false });

            AssertRequest(httpClient, Method.Get, "domains/{domain}/inboxes", "inbox", "*", "full", "False");
        }

        [TestMethod]
        public async Task GetDomainAsync_BuildsGetRequestWithDomainUrlSegment()
        {
            var httpClient = new RecordingHttpClient();
            var client = new DomainsClient(httpClient, "domains");

            await client.GetDomainAsync(new GetDomainRequest { DomainId = "example.com" });

            Assert.AreEqual(Method.Get, httpClient.Request.Method);
            Assert.AreEqual("domains/{domain_id}", httpClient.Request.Resource);
            Assert.AreEqual("example.com", ParameterValue(httpClient.Request, "domain_id"));
        }

        [TestMethod]
        public async Task FetchInboxAsync_BuildsRouteAndOptionalQueryParameters()
        {
            var httpClient = new RecordingHttpClient();
            var client = new MessagesClient(httpClient, "domains");
            var request = new FetchInboxRequest
            {
                Domain = "example.com",
                Inbox = "orders",
                Skip = 10,
                Limit = 20,
                Sort = Sort.asc,
                DecodeSubject = true,
                Cursor = "next-page",
                Full = true,
                Delete = "30s",
                Wait = "10s"
            };

            await client.FetchInboxAsync(request);

            Assert.AreEqual(Method.Get, httpClient.Request.Method);
            Assert.AreEqual("domains/{domain}/inboxes/{inbox}", httpClient.Request.Resource);
            Assert.AreEqual("example.com", ParameterValue(httpClient.Request, "domain"));
            Assert.AreEqual("orders", ParameterValue(httpClient.Request, "inbox"));
            Assert.AreEqual("10", ParameterValue(httpClient.Request, "skip"));
            Assert.AreEqual("20", ParameterValue(httpClient.Request, "limit"));
            Assert.AreEqual("asc", ParameterValue(httpClient.Request, "sort"));
            Assert.AreEqual("True", ParameterValue(httpClient.Request, "decode_subject"));
            Assert.AreEqual("next-page", ParameterValue(httpClient.Request, "cursor"));
            Assert.AreEqual("True", ParameterValue(httpClient.Request, "full"));
            Assert.AreEqual("30s", ParameterValue(httpClient.Request, "delete"));
            Assert.AreEqual("10s", ParameterValue(httpClient.Request, "wait"));
        }

        [TestMethod]
        public async Task FetchInboxMessageAsync_ForwardsDeleteQueryParameter()
        {
            var httpClient = new RecordingHttpClient();
            var client = new MessagesClient(httpClient, "domains");

            await client.FetchInboxMessageAsync(new FetchInboxMessageRequest
            {
                Domain = "example.com",
                Inbox = "orders",
                MessageId = "message-123",
                Delete = "30s"
            });

            Assert.AreEqual(Method.Get, httpClient.Request.Method);
            Assert.AreEqual("domains/{domain}/inboxes/{inbox}/messages/{messageId}", httpClient.Request.Resource);
            Assert.AreEqual("30s", ParameterValue(httpClient.Request, "delete"));
        }

        [TestMethod]
        public async Task PostMessageAsync_BuildsPostRequestWithJsonBody()
        {
            var httpClient = new RecordingHttpClient();
            var client = new MessagesClient(httpClient, "domains");
            var message = new MessageToPost { From = "sender@example.com", Subject = "Hello", Text = "Body" };

            await client.PostMessageAsync(new PostMessageRequest { Domain = "example.com", Inbox = "orders", Message = message });

            Assert.AreEqual(Method.Post, httpClient.Request.Method);
            Assert.AreEqual("domains/{domain}/inboxes/{inbox}/messages", httpClient.Request.Resource);
            Assert.AreEqual("example.com", ParameterValue(httpClient.Request, "domain"));
            Assert.AreEqual("orders", ParameterValue(httpClient.Request, "inbox"));
            Assert.AreSame(message, httpClient.Request.Parameters.Single(parameter => parameter.Type == ParameterType.RequestBody).Value);
        }

        [TestMethod]
        public async Task AuthenticatorsClient_BuildsRequestsForEveryOperation()
        {
            var httpClient = new RecordingHttpClient();
            var client = new AuthenticatorsClient(httpClient, "");

            await client.InstantTOTP2FACodeAsync(new InstantTOTP2FACodeRequest { TotpSecretKey = "secret" });
            AssertRequest(httpClient, Method.Get, "totp/{totp_secret_key}", "totp_secret_key", "secret");

#pragma warning disable CS0618
            await client.GetAuthenticatorsAsync();
            AssertRequest(httpClient, Method.Get, "authenticators");
#pragma warning restore CS0618

            await client.GetAuthenticatorsByIdAsync(new GetAuthenticatorsByIdRequest { Id = "auth-1" });
            AssertRequest(httpClient, Method.Get, "authenticators/{auth_id}", "auth_id", "auth-1");

#pragma warning disable CS0618
            await client.GetAuthenticatorAsync();
            AssertRequest(httpClient, Method.Get, "authenticator");
            await client.GetAuthenticatorByIdAsync(new GetAuthenticatorByIdRequest { Id = "auth-1" });
            AssertRequest(httpClient, Method.Get, "authenticator/{auth_id}", "auth_id", "auth-1");
#pragma warning restore CS0618
        }

        [TestMethod]
        public async Task DomainsClient_BuildsRequestsForEveryOperation()
        {
            var httpClient = new RecordingHttpClient();
            var client = new DomainsClient(httpClient, "domains");

            await client.GetAllDomainsAsync();
            AssertRequest(httpClient, Method.Get, "domains/");

#pragma warning disable CS0618
            await client.CreateDomainAsync(new CreateDomainRequest { Name = "example.com" });
            AssertRequest(httpClient, Method.Post, "domains/{domain_id}", "domain_id", "example.com");
            await client.DeleteDomainAsync(new DeleteDomainRequest { DomainId = "example.com" });
            AssertRequest(httpClient, Method.Delete, "domains/{domain_id}", "domain_id", "example.com");
#pragma warning restore CS0618
        }

        [TestMethod]
        public async Task MessagesClient_BuildsRequestsForMessageRetrievalOperations()
        {
            var httpClient = new RecordingHttpClient();
            var client = new MessagesClient(httpClient, "domains");

            await client.FetchMessageAsync(new FetchMessageRequest { Domain = "example.com", MessageId = "message-1", Delete = "1m" });
            AssertRequest(httpClient, Method.Get, "domains/{domain}/messages/{messageId}", "domain", "example.com", "messageId", "message-1", "delete", "1m");
            await client.FetchSMSMessagesAsync(new FetchSMSMessagesRequest { Domain = "example.com", TeamSMSNumber = "15551234567" });
            AssertRequest(httpClient, Method.Get, "domains/{domain}/inboxes/{teamSMSNumber}", "domain", "example.com", "teamSMSNumber", "15551234567");
            await client.FetchInboxMessageAttachmentsAsync(new FetchInboxMessageAttachmentsRequest { Domain = "example.com", Inbox = "orders", MessageId = "message-1" });
            AssertRequest(httpClient, Method.Get, "domains/{domain}/inboxes/{inbox}/messages/{messageId}/attachments", "domain", "example.com", "inbox", "orders", "messageId", "message-1");
            await client.FetchMessageAttachmentsAsync(new FetchMessageAttachmentsRequest { Domain = "example.com", MessageId = "message-1" });
            AssertRequest(httpClient, Method.Get, "domains/{domain}/messages/{messageId}/attachments", "domain", "example.com", "messageId", "message-1");
            await client.FetchInboxMessageAttachmentAsync(new FetchInboxMessageAttachmentRequest { Domain = "example.com", Inbox = "orders", MessageId = "message-1", AttachmentId = "file.pdf" });
            AssertRequest(httpClient, Method.Get, "domains/{domain}/inboxes/{inbox}/messages/{messageId}/attachments/{attachmentId}", "domain", "example.com", "inbox", "orders", "messageId", "message-1", "attachmentId", "file.pdf");
            await client.FetchMessageAttachmentAsync(new FetchMessageAttachmentRequest { Domain = "example.com", MessageId = "message-1", AttachmentId = "file.pdf" });
            AssertRequest(httpClient, Method.Get, "domains/{domain}/messages/{messageId}/attachments/{attachmentId}", "domain", "example.com", "messageId", "message-1", "attachmentId", "file.pdf");
            await client.FetchMessageLinksAsync(new FetchMessageLinksRequest { Domain = "example.com", MessageId = "message-1" });
            AssertRequest(httpClient, Method.Get, "domains/{domain}/messages/{messageId}/links", "domain", "example.com", "messageId", "message-1");
            await client.FetchInboxMessageLinksAsync(new FetchInboxMessageLinksRequest { Domain = "example.com", Inbox = "orders", MessageId = "message-1" });
            AssertRequest(httpClient, Method.Get, "domains/{domain}/inboxes/{inbox}/messages/{messageId}/links", "domain", "example.com", "inbox", "orders", "messageId", "message-1");
            await client.FetchMessageLinksFullAsync(new FetchMessageLinksFullRequest { Domain = "example.com", MessageId = "message-1" });
            AssertRequest(httpClient, Method.Get, "domains/{domain}/messages/{messageId}/linksfull", "domain", "example.com", "messageId", "message-1");
        }

        [TestMethod]
        public async Task MessagesClient_BuildsRequestsForDeletionAndContentOperations()
        {
            var httpClient = new RecordingHttpClient();
            var client = new MessagesClient(httpClient, "domains");

            await client.DeleteAllDomainMessagesAsync(new DeleteAllDomainMessagesRequest { Domain = "example.com" });
            AssertRequest(httpClient, Method.Delete, "domains/{domain}/inboxes", "domain", "example.com");
            await client.DeleteAllInboxMessagesAsync(new DeleteAllInboxMessagesRequest { Domain = "example.com", Inbox = "orders" });
            AssertRequest(httpClient, Method.Delete, "domains/{domain}/inboxes/{inbox}", "domain", "example.com", "inbox", "orders");
            await client.DeleteMessageAsync(new DeleteMessageRequest { Domain = "example.com", Inbox = "orders", MessageId = "message-1" });
            AssertRequest(httpClient, Method.Delete, "domains/{domain}/inboxes/{inbox}/messages/{messageId}", "domain", "example.com", "inbox", "orders", "messageId", "message-1");
            await client.FetchMessageSmtpLogAsync(new FetchMessageSmtpLogRequest { Domain = "example.com", MessageId = "message-1" });
            AssertRequest(httpClient, Method.Get, "domains/{domain}/messages/{messageId}/smtplog", "domain", "example.com", "messageId", "message-1");
            await client.FetchInboxMessageSmtpLogAsync(new FetchInboxMessageSmtpLogRequest { Domain = "example.com", Inbox = "orders", MessageId = "message-1" });
            AssertRequest(httpClient, Method.Get, "domains/{domain}/inboxes/{inbox}/messages/{messageId}/smtplog", "domain", "example.com", "inbox", "orders", "messageId", "message-1");
            await client.FetchMessageRawAsync(new FetchMessageRawRequest { Domain = "example.com", MessageId = "message-1" });
            AssertRequest(httpClient, Method.Get, "domains/{domain}/messages/{messageId}/raw", "domain", "example.com", "messageId", "message-1");
            await client.FetchInboxMessageRawAsync(new FetchInboxMessageRawRequest { Domain = "example.com", Inbox = "orders", MessageId = "message-1" });
            AssertRequest(httpClient, Method.Get, "domains/{domain}/inboxes/{inbox}/messages/{messageId}/raw", "domain", "example.com", "inbox", "orders", "messageId", "message-1");

#pragma warning disable CS0618
            await client.FetchLatestMessagesAsync(new FetchLatestMessagesRequest { Domain = "example.com" });
            AssertRequest(httpClient, Method.Get, "domains/{domain}/messages/*", "domain", "example.com");
            await client.FetchLatestInboxMessagesAsync(new FetchLatestInboxMessagesRequest { Domain = "example.com", Inbox = "orders" });
            AssertRequest(httpClient, Method.Get, "domains/{domain}/inboxes/{inbox}/messages/*", "domain", "example.com", "inbox", "orders");
#pragma warning restore CS0618
        }

        [TestMethod]
        public async Task RulesClient_BuildsRequestsForEveryOperation()
        {
            var httpClient = new RecordingHttpClient();
            var client = new RulesClient(httpClient, "domains");
            var createRequest = new CreateRuleRequest { DomainId = "example.com" };

#pragma warning disable CS0618
            await client.CreateRuleAsync(createRequest);
            AssertRequest(httpClient, Method.Post, "domains/{domain_id}/rules", "domain_id", "example.com");
            Assert.AreSame(createRequest.Rule, BodyValue(httpClient.Request));
            await client.EnableRuleAsync(new EnableRuleRequest { DomainId = "example.com", RuleId = "rule-1" });
            AssertRequest(httpClient, Method.Put, "domains/{domain_id}/rules/{ruleId}/enable", "domain_id", "example.com", "ruleId", "rule-1");
            await client.DisableRuleAsync(new DisableRuleRequest { DomainId = "example.com", RuleId = "rule-1" });
            AssertRequest(httpClient, Method.Put, "domains/{domain_id}/rules/{ruleId}/disable", "domain_id", "example.com", "ruleId", "rule-1");
            await client.GetAllRulesAsync(new GetAllRulesRequest { DomainId = "example.com" });
            AssertRequest(httpClient, Method.Get, "domains/{domain_id}/rules", "domain_id", "example.com");
            await client.GetRuleAsync(new GetRuleRequest { DomainId = "example.com", RuleId = "rule-1" });
            AssertRequest(httpClient, Method.Get, "domains/{domain_id}/rules/{ruleId}", "domain_id", "example.com", "ruleId", "rule-1");
            await client.DeleteRuleAsync(new DeleteRuleRequest { DomainId = "example.com", RuleId = "rule-1" });
            AssertRequest(httpClient, Method.Delete, "domains/{domain_id}/rules/{ruleId}", "domain_id", "example.com", "ruleId", "rule-1");
#pragma warning restore CS0618
        }

        [TestMethod]
        public async Task StatsClient_BuildsRequestsForEveryOperation()
        {
            var httpClient = new RecordingHttpClient();
            var client = new StatsClient(httpClient, "stats");

            await client.GetTeamStatsAsync();
            AssertRequest(httpClient, Method.Get, "statsteam/stats");
            await client.GetTeamAsync();
            AssertRequest(httpClient, Method.Get, "stats/team");
            await client.GetTeamInfoAsync();
            AssertRequest(httpClient, Method.Get, "stats/teaminfo");
        }

        [TestMethod]
        public async Task WebhooksClient_BuildsRequestsForEveryOperation()
        {
            var httpClient = new RecordingHttpClient();
            var client = new WebhooksClient(httpClient, "webhooks");
            var webhook = new Webhook { From = "sender@example.com", Subject = "Hello" };

            await client.PrivateWebhookAsync(new PrivateWebhookRequest { WebhookToken = "token", Webhook = webhook });
            AssertRequest(httpClient, Method.Post, "webhooks/private/webhook", "whtoken", "token");
            Assert.AreSame(webhook, BodyValue(httpClient.Request));
            await client.PrivateInboxWebhookAsync(new PrivateInboxWebhookRequest { WebhookToken = "token", Inbox = "orders", Webhook = webhook });
            AssertRequest(httpClient, Method.Post, "webhooks/private/webhook/{inbox}", "whtoken", "token", "inbox", "orders");
            await client.PrivateCustomServiceWebhookAsync(new PrivateCustomServiceWebhookRequest { WebhookToken = "token", CustomService = "twilio", Webhook = webhook });
            AssertRequest(httpClient, Method.Post, "webhooks/private/{customService}", "whtoken", "token", "customService", "twilio");
            await client.PrivateCustomServiceInboxWebhookAsync(new PrivateCustomServiceInboxWebhookRequest { WebhookToken = "token", CustomService = "twilio", Inbox = "orders", Webhook = webhook });
            AssertRequest(httpClient, Method.Post, "webhooks/private/{customService}/{inbox}", "whtoken", "token", "customService", "twilio", "inbox", "orders");
        }

        private static object ParameterValue(RestRequest request, string name)
        {
            return request.Parameters.Single(parameter => parameter.Name == name).Value;
        }

        private static object BodyValue(RestRequest request)
        {
            return request.Parameters.Single(parameter => parameter.Type == ParameterType.RequestBody).Value;
        }

        private static void AssertRequest(RecordingHttpClient httpClient, Method method, string resource, params string[] parameters)
        {
            Assert.AreEqual(method, httpClient.Request.Method);
            Assert.AreEqual(resource, httpClient.Request.Resource);
            Assert.AreEqual(0, parameters.Length % 2, "Expected parameter names and values in pairs.");

            for (var index = 0; index < parameters.Length; index += 2)
            {
                Assert.AreEqual(parameters[index + 1], ParameterValue(httpClient.Request, parameters[index])?.ToString());
            }
        }

        private sealed class RecordingHttpClient : IHttpClient
        {
            public RestRequest Request { get; private set; }

            public RestRequest GetRequest(string url, Method method)
            {
                return new RestRequest(url, method);
            }

            public Task<T> ExecuteAsync<T>(RestRequest request)
            {
                Request = request;
                return Task.FromResult(default(T));
            }

            public Task<T> ExecuteAsync<T>(RestRequest request, Func<RestResponse, T> customDeserializationFunction)
            {
                Request = request;
                return Task.FromResult(default(T));
            }
        }
    }
}
