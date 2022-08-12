using mailinator_csharp_client.Clients.HttpClient;
using mailinator_csharp_client.Helpers;
using mailinator_csharp_client.Models.Messages.Requests;
using mailinator_csharp_client.Models.Responses;
using mailinator_csharp_client.Models.Stats.Responses;
using RestSharp;
using System.IO;
using System.Threading.Tasks;

namespace mailinator_csharp_client.Clients.ApiClients.Stats
{
    /// <summary>
    /// Client for Stats API
    /// </summary>
    public class StatsClient : IApiClient
    {
        private readonly IHttpClient httpClient;
        private readonly string endpointUrl;

        public StatsClient(IHttpClient httpClient, string endpointUrl)
        {
            this.httpClient = httpClient;
            this.endpointUrl = endpointUrl;
        }

        /// <summary>
        /// This endpoint retrieves stats of team.
        /// </summary>
        /// <returns></returns>
        public async Task<GetTeamStatsResponse> GetTeamStatsAsync()
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "/stats", Method.Get);
            
            var response = await httpClient.ExecuteAsync<GetTeamStatsResponse>(requestObject);
            return response;
        }

        /// <summary>
        /// This endpoint retrieves team info.
        /// </summary>
        /// <returns></returns>
        public async Task<GetTeamResponse> GetTeamAsync()
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "/", Method.Get);

            var response = await httpClient.ExecuteAsync<GetTeamResponse>(requestObject);
            return response;
        }
    }
}
