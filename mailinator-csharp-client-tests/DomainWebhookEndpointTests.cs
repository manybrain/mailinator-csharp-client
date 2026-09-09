using System;
using System.Threading.Tasks;
using mailinator_csharp_client;
using mailinator_csharp_client.Helpers;
using mailinator_csharp_client.Models.Webhooks.Entities;
using mailinator_csharp_client.Models.Webhooks.Requests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mailinator_csharp_client_tests
{
    // Explicitly opt in: each test case creates one message and does not delete it.
    [TestClass]
    public class DomainWebhookEndpointTests
    {
        [TestMethod, TestCategory("Webhooks.PostWebhookMessageAsync")]
        [DataRow(false)]
        [DataRow(true)]
        public async Task PostWebhookMessageAsync(bool tokenInPath)
        {
            if (Environment.GetEnvironmentVariable("MAILINATOR_TEST_RUN_WEBHOOKS") != "1")
                Assert.Inconclusive("Set MAILINATOR_TEST_RUN_WEBHOOKS=1 to authorize creating test messages.");
            var domain = Environment.GetEnvironmentVariable("MAILINATOR_TEST_DOMAIN_PRIVATE");
            var token = Environment.GetEnvironmentVariable("MAILINATOR_TEST_WEBHOOKTOKEN_PRIVATEDOMAIN");
            var inbox = Environment.GetEnvironmentVariable("MAILINATOR_TEST_WEBHOOK_INBOX");
            if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(inbox) ||
                (!tokenInPath && string.IsNullOrWhiteSpace(domain)))
                Assert.Inconclusive("Configure the private domain, webhook token, and webhook inbox process environment variables.");

            var client = new MailinatorClient();
            try
            {
                var response = await client.WebhooksClient.PostWebhookMessageAsync(new PostWebhookMessageRequest
                {
                    Domain = tokenInPath ? token : domain,
                    WebhookToken = tokenInPath ? null : token,
                    Webhook = new WebhookMessage
                    {
                        To = inbox, From = "sdk-test@example.com",
                        Subject = "C# webhook test " + Guid.NewGuid().ToString("N"),
                        Text = "Webhook integration test", Html = "<p>Webhook integration test</p>"
                    }
                });
                Assert.IsNotNull(response);
                Assert.IsTrue(response.Status == "ok", "The webhook should be accepted.");
                Assert.IsFalse(string.IsNullOrWhiteSpace(response.Id), "The response should include a message ID.");
            }
            catch (ApiException exception)
            {
                // Never include token-bearing URLs or API response bodies in test output.
                Assert.Fail($"Webhook injection failed with HTTP status {(int)exception.HttpStatusCode}.");
            }
        }

        [TestMethod, TestCategory("Webhooks.PostWebhookInboxMessageAsync")]
        [DataRow(false)]
        [DataRow(true)]
        public async Task PostWebhookInboxMessageAsync(bool tokenInPath)
        {
            if (Environment.GetEnvironmentVariable("MAILINATOR_TEST_RUN_WEBHOOKS") != "1")
                Assert.Inconclusive("Set MAILINATOR_TEST_RUN_WEBHOOKS=1 to authorize creating test messages.");
            var domain = Environment.GetEnvironmentVariable("MAILINATOR_TEST_DOMAIN_PRIVATE");
            var token = Environment.GetEnvironmentVariable("MAILINATOR_TEST_WEBHOOKTOKEN_PRIVATEDOMAIN");
            var inbox = Environment.GetEnvironmentVariable("MAILINATOR_TEST_WEBHOOK_INBOX");
            if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(inbox) ||
                (!tokenInPath && string.IsNullOrWhiteSpace(domain)))
                Assert.Inconclusive("Configure the private domain, webhook token, and webhook inbox process environment variables.");

            var client = new MailinatorClient();
            try
            {
                var response = await client.WebhooksClient.PostWebhookInboxMessageAsync(new PostWebhookInboxMessageRequest
                {
                    Domain = tokenInPath ? token : domain,
                    WebhookToken = tokenInPath ? null : token,
                    Inbox = inbox,
                    Webhook = new WebhookMessage
                    {
                        To = inbox, From = "sdk-test@example.com",
                        Subject = "C# webhook test " + Guid.NewGuid().ToString("N"),
                        Text = "Webhook integration test", Html = "<p>Webhook integration test</p>"
                    }
                });
                Assert.IsNotNull(response);
                Assert.IsTrue(response.Status == "ok", "The webhook should be accepted.");
                Assert.IsFalse(string.IsNullOrWhiteSpace(response.Id), "The response should include a message ID.");
            }
            catch (ApiException exception)
            {
                // Never include token-bearing URLs or API response bodies in test output.
                Assert.Fail($"Webhook injection failed with HTTP status {(int)exception.HttpStatusCode}.");
            }
        }

    }
}
