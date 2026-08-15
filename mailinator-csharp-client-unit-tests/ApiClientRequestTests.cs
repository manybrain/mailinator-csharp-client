using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mailinator_csharp_client.Clients.ApiClients.Domains;
using mailinator_csharp_client.Clients.ApiClients.Messages;
using mailinator_csharp_client.Clients.HttpClient;
using mailinator_csharp_client.Models.Domains.Requests;
using mailinator_csharp_client.Models.Domains.Responses;
using mailinator_csharp_client.Models.Messages.Entities;
using mailinator_csharp_client.Models.Messages.Requests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace mailinator_csharp_client_unit_tests
{
    [TestClass]
    public class ApiClientRequestTests
    {
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

        private static object ParameterValue(RestRequest request, string name)
        {
            return request.Parameters.Single(parameter => parameter.Name == name).Value;
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
