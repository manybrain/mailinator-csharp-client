using System;
using System.Linq;
using System.Threading.Tasks;
using mailinator_csharp_client.Clients.ApiClients.Messages;
using mailinator_csharp_client.Clients.HttpClient;
using mailinator_csharp_client.Models.Messages.Requests;
using mailinator_csharp_client.Models.Responses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace mailinator_csharp_client_unit_tests
{
    [TestClass]
    public class MessageContentTests
    {
        [TestMethod]
        public async Task GetMessageTextAsync_BuildsGetRequestAndReturnsResponse()
        {
            var expected = new GetMessageTextResponse { Text = "content" };
            var http = new ContentHttpClient(expected);
            var client = new MessagesClient(http, "domains");

            var response = await client.GetMessageTextAsync(new GetMessageTextRequest
            {
                Domain = "example.com", MessageId = "message+123/part"
            });

            Assert.AreEqual(Method.Get, http.Request.Method);
            Assert.AreEqual("domains/{domain}/messages/{messageId}/text", http.Request.Resource);
            Assert.AreEqual(2, http.Request.Parameters.Count());
            Assert.IsTrue(http.Request.Parameters.All(p => p.Type == ParameterType.UrlSegment));
            Assert.AreEqual("example.com", http.Request.Parameters.Single(p => p.Name == "domain").Value);
            Assert.AreEqual("message+123/part", http.Request.Parameters.Single(p => p.Name == "messageId").Value);
            Assert.AreSame(expected, response);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("Hello\r\nWorld =C2=A0 café <p>HTML &amp; text</p>")]
        public void GetMessageTextResponse_DeserializesContentWithoutTransformingIt(string content)
        {
            var json = new JObject { ["text"] = content }.ToString();
            var response = JsonConvert.DeserializeObject<GetMessageTextResponse>(json);

            Assert.AreEqual(content, response.Text);
        }

        [TestMethod]
        public async Task GetMessageTextPlainAsync_BuildsGetRequestAndReturnsResponse()
        {
            var expected = new GetMessageTextPlainResponse { TextPlain = "content" };
            var http = new ContentHttpClient(expected);
            var client = new MessagesClient(http, "domains");

            var response = await client.GetMessageTextPlainAsync(new GetMessageTextPlainRequest
            {
                Domain = "example.com", MessageId = "message+123/part"
            });

            Assert.AreEqual(Method.Get, http.Request.Method);
            Assert.AreEqual("domains/{domain}/messages/{messageId}/textplain", http.Request.Resource);
            Assert.AreEqual(2, http.Request.Parameters.Count());
            Assert.IsTrue(http.Request.Parameters.All(p => p.Type == ParameterType.UrlSegment));
            Assert.AreEqual("example.com", http.Request.Parameters.Single(p => p.Name == "domain").Value);
            Assert.AreEqual("message+123/part", http.Request.Parameters.Single(p => p.Name == "messageId").Value);
            Assert.AreSame(expected, response);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("Hello\r\nWorld =C2=A0 café <p>HTML &amp; text</p>")]
        public void GetMessageTextPlainResponse_DeserializesContentWithoutTransformingIt(string content)
        {
            var json = new JObject { ["text/plain"] = content }.ToString();
            var response = JsonConvert.DeserializeObject<GetMessageTextPlainResponse>(json);

            Assert.AreEqual(content, response.TextPlain);
        }

        [TestMethod]
        public async Task GetMessageTextHtmlAsync_BuildsGetRequestAndReturnsResponse()
        {
            var expected = new GetMessageTextHtmlResponse { TextHtml = "content" };
            var http = new ContentHttpClient(expected);
            var client = new MessagesClient(http, "domains");

            var response = await client.GetMessageTextHtmlAsync(new GetMessageTextHtmlRequest
            {
                Domain = "example.com", MessageId = "message+123/part"
            });

            Assert.AreEqual(Method.Get, http.Request.Method);
            Assert.AreEqual("domains/{domain}/messages/{messageId}/texthtml", http.Request.Resource);
            Assert.AreEqual(2, http.Request.Parameters.Count());
            Assert.IsTrue(http.Request.Parameters.All(p => p.Type == ParameterType.UrlSegment));
            Assert.AreEqual("example.com", http.Request.Parameters.Single(p => p.Name == "domain").Value);
            Assert.AreEqual("message+123/part", http.Request.Parameters.Single(p => p.Name == "messageId").Value);
            Assert.AreSame(expected, response);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("Hello\r\nWorld =C2=A0 café <p>HTML &amp; text</p>")]
        public void GetMessageTextHtmlResponse_DeserializesContentWithoutTransformingIt(string content)
        {
            var json = new JObject { ["text/html"] = content }.ToString();
            var response = JsonConvert.DeserializeObject<GetMessageTextHtmlResponse>(json);

            Assert.AreEqual(content, response.TextHtml);
        }

        private sealed class ContentHttpClient : IHttpClient
        {
            private readonly object response;
            public RestRequest Request { get; private set; }
            public ContentHttpClient(object response) => this.response = response;
            public RestRequest GetRequest(string url, Method method) => new RestRequest(url, method);
            public Task<T> ExecuteAsync<T>(RestRequest request)
            {
                Request = request;
                return Task.FromResult((T)response);
            }
            public Task<T> ExecuteAsync<T>(RestRequest request, Func<RestResponse, T> customDeserializationFunction)
                => throw new NotSupportedException();
        }
    }
}
