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
    public class MessageHeadersTests
    {
        [TestMethod]
        public async Task GetMessageHeadersAsync_BuildsGetRequestAndReturnsResponse()
        {
            var http = new HeadersHttpClient();
            var client = new MessagesClient(http, "domains");

            var response = await client.GetMessageHeadersAsync(new GetMessageHeadersRequest
            {
                Domain = "example.com", MessageId = "message+123"
            });

            Assert.AreEqual(Method.Get, http.Request.Method);
            Assert.AreEqual("domains/{domain}/messages/{messageId}/headers", http.Request.Resource);
            Assert.AreEqual(2, http.Request.Parameters.Count());
            Assert.IsTrue(http.Request.Parameters.All(p => p.Type == ParameterType.UrlSegment));
            Assert.AreEqual("example.com", http.Request.Parameters.Single(p => p.Name == "domain").Value);
            Assert.AreEqual("message+123", http.Request.Parameters.Single(p => p.Name == "messageId").Value);
            Assert.AreSame(http.Response, response);
        }

        [TestMethod]
        public void GetMessageHeadersResponse_DeserializesStringsArraysAndCustomHeaders()
        {
            var response = JsonConvert.DeserializeObject<GetMessageHeadersResponse>(
                "{\"headers\":{\"subject\":\"Test\",\"received\":[\"hop one\",\"hop two\"],\"x-custom\":\"custom value\"}}");

            Assert.AreEqual("Test", response.Headers["subject"]);
            CollectionAssert.AreEqual(new[] { "hop one", "hop two" }, ((JArray)response.Headers["received"]).ToObject<string[]>());
            Assert.AreEqual("custom value", response.Headers["x-custom"]);
        }

        [TestMethod]
        public void GetMessageHeadersResponse_DeserializesEmptyHeaders()
        {
            var response = JsonConvert.DeserializeObject<GetMessageHeadersResponse>("{\"headers\":{}}");
            Assert.IsNotNull(response.Headers);
            Assert.AreEqual(0, response.Headers.Count);
        }

        private sealed class HeadersHttpClient : IHttpClient
        {
            public RestRequest Request { get; private set; }
            public GetMessageHeadersResponse Response { get; } = new GetMessageHeadersResponse();
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
