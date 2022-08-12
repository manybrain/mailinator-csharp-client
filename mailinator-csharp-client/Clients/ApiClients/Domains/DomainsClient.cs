using mailinator_csharp_client.Clients.HttpClient;
using mailinator_csharp_client.Models.Domains.Requests;
using mailinator_csharp_client.Models.Domains.Responses;
using RestSharp;
using System.Threading.Tasks;

namespace mailinator_csharp_client.Clients.ApiClients.Domains
{
    /// <summary>
    /// Client for Domains API.
    /// 
    /// You may add or replace Private Domains in your Team Settings panel.
    /// </summary>
    public class DomainsClient : IApiClient
    {
        private readonly IHttpClient httpClient;
        private readonly string endpointUrl;

        public DomainsClient(IHttpClient httpClient, string endpointUrl)
        {
            this.httpClient = httpClient;
            this.endpointUrl = endpointUrl;
        }

        /// <summary>
        /// The endpoint fetches a list of all your domains.
        /// </summary>
        /// <returns></returns>
        public async Task<GetAllDomainsResponse> GetAllDomainsAsync()
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "/", Method.Get);

            var response = await httpClient.ExecuteAsync<GetAllDomainsResponse>(requestObject);
            return response;
        }

        /// <summary>
        /// The endpoint fetches a specific domain
        /// </summary>
        /// <param name="request">GetDomainRequest object.</param>
        /// <returns></returns>
        public async Task<GetDomainResponse> GetDomainAsync(GetDomainRequest request)
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "/{domain_id}", Method.Get);
            requestObject.AddUrlSegment("domain_id", request.DomainId);

            var response = await httpClient.ExecuteAsync<GetDomainResponse>(requestObject);
            return response;
        }
    }
}
