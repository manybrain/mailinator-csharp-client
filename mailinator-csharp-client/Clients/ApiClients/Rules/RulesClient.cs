using mailinator_csharp_client.Clients.HttpClient;
using mailinator_csharp_client.Helpers;
using mailinator_csharp_client.Models.Rules.Requests;
using mailinator_csharp_client.Models.Rules.Responses;
using RestSharp;
using System.Threading.Tasks;

namespace mailinator_csharp_client.Clients.ApiClients.Rules
{
    /// <summary>
    /// Client for Rules API.
    /// 
    /// You may define domain-specific rules to process incoming messages. 
    /// Rules are executed in priority order (Rules with equal priority run simultaneously).
    /// Rules contain one or more conditions and one or more actions.
    /// </summary>
    public class RulesClient : IApiClient
    {
        private readonly IHttpClient httpClient;
        private readonly string endpointUrl;

        public RulesClient(IHttpClient httpClient, string endpointUrl)
        {
            this.httpClient = httpClient;
            this.endpointUrl = endpointUrl;
        }

        /// <summary>
        /// This endpoint allows you to create a Rule. Note that in the examples, ":domain_id" can be one of your private domains.
        /// </summary>
        /// <param name="request">CreateRuleRequest object.</param>
        /// <returns></returns>
        public async Task<CreateRuleResponse> CreateRuleAsync(CreateRuleRequest request)
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "/{domain_id}/rules", Method.POST);
            requestObject.AddUrlSegment("domain_id", request.DomainId);

            requestObject.JsonSerializer = new DynamicJsonSerializer();

            requestObject.AddJsonBody(request.Rule);

            var response = await httpClient.ExecuteAsync<CreateRuleResponse>(requestObject);
            return response;
        }

        /// <summary>
        /// This endpoint allows you to enable an existing Rule
        /// </summary>
        /// <param name="request">EnableRuleRequest object.</param>
        /// <returns></returns>
        public async Task<EnableRuleResponse> EnableRuleAsync(EnableRuleRequest request)
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "/{domain_id}/rules/{ruleId}/enable", Method.PUT);
            requestObject.AddUrlSegment("domain_id", request.DomainId);
            requestObject.AddUrlSegment("ruleId", request.RuleId);

            var response = await httpClient.ExecuteAsync<EnableRuleResponse>(requestObject);
            return response;
        }

        /// <summary>
        /// This endpoint allows you to disable an existing Rule
        /// </summary>
        /// <param name="request">DisableRuleRequest object.</param>
        /// <returns></returns>
        public async Task<DisableRuleResponse> DisableRuleAsync(DisableRuleRequest request)
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "/{domain_id}/rules/{ruleId}/disable", Method.PUT);
            requestObject.AddUrlSegment("domain_id", request.DomainId);
            requestObject.AddUrlSegment("ruleId", request.RuleId);

            var response = await httpClient.ExecuteAsync<DisableRuleResponse>(requestObject);
            return response;
        }

        /// <summary>
        /// This endpoint fetches all Rules for a Domain
        /// </summary>
        /// <param name="request">GetAllRulesRequest object.</param>
        /// <returns></returns>
        public async Task<GetAllRulesResponse> GetAllRulesAsync(GetAllRulesRequest request)
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "/{domain_id}/rules", Method.GET);
            requestObject.AddUrlSegment("domain_id", request.DomainId);

            var response = await httpClient.ExecuteAsync<GetAllRulesResponse>(requestObject);
            return response;
        }

        /// <summary>
        /// This endpoint fetches a Rules for a Domain
        /// </summary>
        /// <param name="request">GetRuleRequest object.</param>
        /// <returns></returns>
        public async Task<GetRuleResponse> GetRuleAsync(GetRuleRequest request)
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "/{domain_id}/rules/{ruleId}", Method.GET);
            requestObject.AddUrlSegment("domain_id", request.DomainId);
            requestObject.AddUrlSegment("ruleId", request.RuleId);

            var response = await httpClient.ExecuteAsync<GetRuleResponse>(requestObject);
            return response;
        }

        /// <summary>
        /// This endpoint deletes a specific Rule from a Domain
        /// </summary>
        /// <param name="request">DeleteRuleRequest object.</param>
        /// <returns></returns>
        public async Task<DeleteRuleResponse> DeleteRuleAsync(DeleteRuleRequest request)
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "/{domain_id}/rules/{ruleId}", Method.DELETE);
            requestObject.AddUrlSegment("domain_id", request.DomainId);
            requestObject.AddUrlSegment("ruleId", request.RuleId);

            var response = await httpClient.ExecuteAsync<DeleteRuleResponse>(requestObject);
            return response;
        }
    }
}
