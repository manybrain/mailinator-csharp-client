using mailinator_csharp_client.Clients.HttpClient;
using mailinator_csharp_client.Models.Authenticators.Requests;
using mailinator_csharp_client.Models.Authenticators.Responses;
using RestSharp;
using System.Threading.Tasks;

namespace mailinator_csharp_client.Clients.ApiClients.Authenticators
{
    /// <summary>
    /// Client for Authenticators API.
    /// 
    /// Get Authenticator Codes via API
    /// </summary>
    public class AuthenticatorsClient : IApiClient
    {
        private readonly IHttpClient httpClient;
        private readonly string endpointUrl;

        public AuthenticatorsClient(IHttpClient httpClient, string endpointUrl)
        {
            this.httpClient = httpClient;
            this.endpointUrl = endpointUrl;
        }

        /// <summary>
        /// Instant TOTP 2FA code
        /// </summary>
        /// <param name="request">InstantTOTP2FACodeRequest object.</param>
        /// <returns></returns>
        public async Task<InstantTOTP2FACodeResponse> InstantTOTP2FACodeAsync(InstantTOTP2FACodeRequest request)
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "totp/{totp_secret_key}", Method.Get);

            requestObject.AddUrlSegment("totp_secret_key", request.TotpSecretKey);

            var response = await httpClient.ExecuteAsync<InstantTOTP2FACodeResponse>(requestObject);
            return response;
        }

        /// <summary>
        /// Fetch Authenticators
        /// </summary>
        /// <returns></returns>
        public async Task<GetAuthenticatorsResponse> GetAuthenticatorsAsync()
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "authenticators", Method.Get);

            var response = await httpClient.ExecuteAsync<GetAuthenticatorsResponse>(requestObject);
            return response;
        }

        /// <summary>
        /// Fetch the TOTP 2FA code from one of your saved Keys
        /// </summary>
        /// <param name="request">GetAuthenticatorsByIdRequest object.</param>
        /// <returns></returns>
        public async Task<GetAuthenticatorsByIdResponse> GetAuthenticatorsByIdAsync(GetAuthenticatorsByIdRequest request)
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "authenticators/{auth_id}", Method.Get);

            requestObject.AddUrlSegment("auth_id", request.Id);

            var response = await httpClient.ExecuteAsync<GetAuthenticatorsByIdResponse>(requestObject);
            return response;
        }


        /// <summary>
        /// Fetch Authenticator
        /// </summary>
        /// <returns></returns>
        public async Task<GetAuthenticatorResponse> GetAuthenticatorAsync()
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "authenticator", Method.Get);

            var response = await httpClient.ExecuteAsync<GetAuthenticatorResponse>(requestObject);
            return response;
        }

        /// <summary>
        /// Fetch Authenticator By Id
        /// </summary>
        /// <param name="request">GetAuthenticatorByIdRequest object.</param>
        /// <returns></returns>
        public async Task<GetAuthenticatorByIdResponse> GetAuthenticatorByIdAsync(GetAuthenticatorByIdRequest request)
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "authenticator/{auth_id}", Method.Get);

            requestObject.AddUrlSegment("auth_id", request.Id);

            var response = await httpClient.ExecuteAsync<GetAuthenticatorByIdResponse>(requestObject);
            return response;
        }
    }
}
