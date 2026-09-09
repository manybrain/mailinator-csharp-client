using System;
using System.Linq;
using System.Threading.Tasks;
using mailinator_csharp_client.Clients.ApiClients.Messages;
using mailinator_csharp_client.Clients.HttpClient;
using mailinator_csharp_client.Models.Messages.Requests;
using mailinator_csharp_client.Models.Responses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;

namespace mailinator_csharp_client_unit_tests
{
    [TestClass]
    public class MessageSummaryTests
    {
        [TestMethod]
        public async Task GetMessageSummaryAsync_BuildsGetRequestAndReturnsResponse()
        {
            var http = new SummaryHttpClient();
            var client = new MessagesClient(http, "domains");

            var response = await client.GetMessageSummaryAsync(new GetMessageSummaryRequest
            {
                Domain = "example.com", MessageId = "message+123"
            });

            Assert.AreEqual(Method.Get, http.Request.Method);
            Assert.AreEqual("domains/{domain}/messages/{messageId}/summary", http.Request.Resource);
            Assert.AreEqual(2, http.Request.Parameters.Count());
            Assert.IsTrue(http.Request.Parameters.All(p => p.Type == ParameterType.UrlSegment));
            Assert.AreEqual("example.com", http.Request.Parameters.Single(p => p.Name == "domain").Value);
            Assert.AreEqual("message+123", http.Request.Parameters.Single(p => p.Name == "messageId").Value);
            Assert.AreSame(http.Response, response);
        }

        [TestMethod]
        public void GetMessageSummaryResponse_DeserializesWrappedMetadataAnd64BitTime()
        {
            var response = JsonConvert.DeserializeObject<GetMessageSummaryResponse>(
                "{\"summary\":{\"subject\":\"Test\",\"domain\":\"example.com\",\"from\":\"sender@example.com\",\"id\":\"message-123\",\"to\":\"orders\",\"time\":1788909009000}}");

            Assert.IsNotNull(response.Summary);
            Assert.AreEqual("Test", response.Summary.Subject);
            Assert.AreEqual("example.com", response.Summary.Domain);
            Assert.AreEqual("sender@example.com", response.Summary.From);
            Assert.AreEqual("message-123", response.Summary.Id);
            Assert.AreEqual("orders", response.Summary.To);
            Assert.AreEqual(1788909009000L, response.Summary.Time);
            Assert.IsNull(response.Summary.Parts);
            Assert.IsNull(response.Summary.Text);
        }
        private sealed class SummaryHttpClient : IHttpClient
        {
            public RestRequest Request { get; private set; }
            public GetMessageSummaryResponse Response { get; } = new GetMessageSummaryResponse();
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

