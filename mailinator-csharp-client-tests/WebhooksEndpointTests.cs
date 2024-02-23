using mailinator_csharp_client.Models.Webhooks.Entities;
using mailinator_csharp_client.Models.Webhooks.Requests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace mailinator_csharp_client_tests
{
    [TestClass]
    public class WebhooksEndpointTests : TestBase
    {
        private readonly Webhook WebhookToAdd = new Webhook { From = "MyMailinatorCSharpTest", Subject = "testing message", Text = "hello world", To = "jack" };

        [TestMethod, TestCategory("Webhooks.PublicWebhookAsync")]
        public async Task PublicWebhookAsync()
        {
            var request = new PublicWebhookRequest() { Webhook = WebhookToAdd };
            var response = await mailinatorClient.WebhooksClient.PublicWebhookAsync(request);
            Assert.IsTrue(response != null);
            Assert.IsTrue(response.Status == "ok");
        }

        [TestMethod, TestCategory("Webhooks.PublicInboxWebhookAsync")]
        public async Task PublicInboxWebhookAsync()
        {
            var request = new PublicInboxWebhookRequest() { Inbox = WebhookInbox, Webhook = WebhookToAdd };
            var response = await mailinatorClient.WebhooksClient.PublicInboxWebhookAsync(request);
            Assert.IsTrue(response != null);
            Assert.IsTrue(response.Status == "ok");
        }

        [TestMethod, TestCategory("Webhooks.PublicCustomServiceWebhookAsync")]
        public async Task PublicCustomServiceWebhookAsync()
        {
            var request = new PublicCustomServiceWebhookRequest() { CustomService = WebhookCustomService, Webhook = WebhookToAdd };
            var response = await mailinatorClient.WebhooksClient.PublicCustomServiceWebhookAsync(request);
            //Assert.IsTrue(response != null);
        }

        [TestMethod, TestCategory("Webhooks.PublicCustomServiceInboxWebhookAsync")]
        public async Task PublicCustomServiceInboxWebhookAsync()
        {
            var request = new PublicCustomServiceInboxWebhookRequest() { CustomService = WebhookCustomService, Inbox = WebhookInbox, Webhook = WebhookToAdd };
            var response = await mailinatorClient.WebhooksClient.PublicCustomServiceInboxWebhookAsync(request);
            //Assert.IsTrue(response != null);
        }

        [TestMethod, TestCategory("Webhooks.PrivateWebhookAsync")]
        public async Task PrivateWebhookAsync()
        {
            var request = new PrivateWebhookRequest() { WebhookToken = WebhookTokenPrivateDomain, Webhook = WebhookToAdd };
            var response = await mailinatorClient.WebhooksClient.PrivateWebhookAsync(request);
            Assert.IsTrue(response != null);
            Assert.IsTrue(response.Status == "ok");
        }

        [TestMethod, TestCategory("Webhooks.PrivateInboxWebhookAsync")]
        public async Task PrivateInboxWebhookAsync()
        {
            var request = new PrivateInboxWebhookRequest() { WebhookToken = WebhookTokenPrivateDomain, Inbox = WebhookInbox, Webhook = WebhookToAdd };
            var response = await mailinatorClient.WebhooksClient.PrivateInboxWebhookAsync(request);
            Assert.IsTrue(response != null);
            Assert.IsTrue(response.Status == "ok");
        }

        [TestMethod, TestCategory("Webhooks.PrivateCustomServiceWebhookAsync")]
        public async Task PrivateCustomServiceWebhookAsync()
        {
            var request = new PrivateCustomServiceWebhookRequest() { WebhookToken = WebhookTokenCustomService, CustomService = WebhookCustomService, Webhook = WebhookToAdd };
            var response = await mailinatorClient.WebhooksClient.PrivateCustomServiceWebhookAsync(request);
            //Assert.IsTrue(response != null);
        }

        [TestMethod, TestCategory("Webhooks.PrivateCustomServiceInboxWebhookAsync")]
        public async Task PrivateCustomServiceInboxWebhookAsync()
        {
            var request = new PrivateCustomServiceInboxWebhookRequest() { WebhookToken = WebhookTokenCustomService, CustomService = WebhookCustomService, Inbox = WebhookInbox, Webhook = WebhookToAdd };
            var response = await mailinatorClient.WebhooksClient.PrivateCustomServiceInboxWebhookAsync(request);
            //Assert.IsTrue(response != null);
        }
    }
}
