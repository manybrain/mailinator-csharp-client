using System;
using System.Linq;
using System.Threading.Tasks;
using mailinator_csharp_client.Clients.ApiClients.Webhooks;
using mailinator_csharp_client.Clients.HttpClient;
using mailinator_csharp_client.Models.Webhooks.Entities;
using mailinator_csharp_client.Models.Webhooks.Requests;
using mailinator_csharp_client.Models.Webhooks.Responses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace mailinator_csharp_client_unit_tests
{
    [TestClass]
    public class WebhookMessageTests
    {
        [TestMethod]
        [DataRow("example.com", "test-token+123")]
        [DataRow("test-token+123", null)]
        public async Task PostWebhookMessageAsync_BuildsRequestForBothAuthenticationForms(string domain, string token)
        {
            var http = new RecordingHttpClient();
            var client = new WebhooksClient(http, "domains");
            var body = new WebhookMessage { To = "orders", Text = "Hello" };
            var response = await client.PostWebhookMessageAsync(new PostWebhookMessageRequest
            {
                Domain = domain, WebhookToken = token, Webhook = body
            });
            Assert.AreEqual(Method.Post, http.Request.Method);
            Assert.AreEqual("domains/{domain}/webhook", http.Request.Resource);
            Assert.AreEqual(domain, http.Request.Parameters.Single(p => p.Name == "domain" && p.Type == ParameterType.UrlSegment).Value);

            var query = http.Request.Parameters.Where(p => p.Type == ParameterType.QueryString).ToArray();
            Assert.AreEqual(token == null ? 0 : 1, query.Length);
            if (token != null)
            {
                Assert.AreEqual("whtoken", query[0].Name);
                Assert.AreEqual(token, query[0].Value);
            }
            Assert.AreSame(body, http.Request.Parameters.Single(p => p.Type == ParameterType.RequestBody).Value);
            Assert.IsFalse(http.Request.Parameters.Any(p => p.Name == "token"));
            Assert.AreSame(http.Response, response);
        }

        [TestMethod]
        [DataRow("example.com", "test-token+123")]
        [DataRow("test-token+123", null)]
        public async Task PostWebhookInboxMessageAsync_BuildsRequestForBothAuthenticationForms(string domain, string token)
        {
            var http = new RecordingHttpClient();
            var client = new WebhooksClient(http, "domains");
            var body = new WebhookMessage { To = "orders", Text = "Hello" };
            var response = await client.PostWebhookInboxMessageAsync(new PostWebhookInboxMessageRequest
            {
                Domain = domain, WebhookToken = token, Webhook = body, Inbox = "orders+test"
            });
            Assert.AreEqual(Method.Post, http.Request.Method);
            Assert.AreEqual("domains/{domain}/webhook/{inbox}", http.Request.Resource);
            Assert.AreEqual(domain, http.Request.Parameters.Single(p => p.Name == "domain" && p.Type == ParameterType.UrlSegment).Value);
            Assert.AreEqual("orders+test", http.Request.Parameters.Single(p => p.Name == "inbox" && p.Type == ParameterType.UrlSegment).Value);
            var query = http.Request.Parameters.Where(p => p.Type == ParameterType.QueryString).ToArray();
            Assert.AreEqual(token == null ? 0 : 1, query.Length);
            if (token != null)
            {
                Assert.AreEqual("whtoken", query[0].Name);
                Assert.AreEqual(token, query[0].Value);
            }
            Assert.AreSame(body, http.Request.Parameters.Single(p => p.Type == ParameterType.RequestBody).Value);
            Assert.IsFalse(http.Request.Parameters.Any(p => p.Name == "token"));
            Assert.AreSame(http.Response, response);
        }

        [TestMethod]
        public void WebhookMessage_PreservesDocumentedAndCustomPayloadFields()
        {
            var body = JsonConvert.DeserializeObject<WebhookMessage>(
                "{\"from\":\"sender\",\"to\":\"orders\",\"subject\":\"Test\",\"text\":\"plain\",\"html\":\"<p>HTML</p>\",\"headers\":{\"X-Test\":\"value\"},\"custom\":{\"count\":2}}");
            Assert.AreEqual("<p>HTML</p>", body.Html);
            Assert.AreEqual("value", body.Headers["X-Test"]);
            var json = JObject.Parse(JsonConvert.SerializeObject(body));
            Assert.AreEqual("sender", (string)json["from"]);
            Assert.AreEqual("orders", (string)json["to"]);
            Assert.AreEqual("Test", (string)json["subject"]);
            Assert.AreEqual("plain", (string)json["text"]);
            Assert.AreEqual(2, (int)json["custom"]["count"]);
        }

        [TestMethod]
        public void PostWebhookMessageResponse_DeserializesStatusAndId()
        {
            var response = JsonConvert.DeserializeObject<PostWebhookMessageResponse>("{\"status\":\"ok\",\"id\":\"message-123\"}");
            Assert.AreEqual("ok", response.Status);
            Assert.AreEqual("message-123", response.Id);
        }

        private sealed class RecordingHttpClient : IHttpClient
        {
            public RestRequest Request { get; private set; }
            public PostWebhookMessageResponse Response { get; } = new PostWebhookMessageResponse();
            public RestRequest GetRequest(string url, Method method) => new RestRequest(url, method);
            public Task<T> ExecuteAsync<T>(RestRequest request)
            {
                Request = request;
                return Task.FromResult((T)(object)Response);
            }
            public Task<T> ExecuteAsync<T>(RestRequest request, Func<RestResponse, T> customDeserializationFunction)
                => throw new NotSupportedException();
        }
    }
}
