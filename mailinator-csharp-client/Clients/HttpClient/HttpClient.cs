using mailinator_csharp_client.Helpers;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace mailinator_csharp_client.Clients.HttpClient
{
    public class HttpClient : IHttpClient
	{
		private const int TIMEOUT_MS = 100000;
		private readonly RestClient restClient;

		public HttpClient(string apiKey, string baseUri)
		{
			var options = new RestClientOptions(baseUri)
			{
				MaxTimeout = TIMEOUT_MS
			};

			restClient = new RestClient(options, configureSerialization: s => s.UseSerializer(() => new DynamicJsonSerializer()));

			restClient.AddDefaultHeader("Authorization", apiKey);
		}

		public RestRequest GetRequest(string url, Method method)
		{
			return new RestRequest(url, method);
		}

		public async Task<T> ExecuteAsync<T>(RestRequest request)
		{
			var response = await restClient.ExecuteAsync<T>(request);

			if (!response.IsSuccessful)
				throw new ApiException(response.StatusCode, response.StatusDescription, response.Content, response.ErrorMessage);

			return response.Data;
		}

		public async Task<T> ExecuteAsync<T>(RestRequest request, Func<RestResponse, T> customDeserializationFunction)
		{
			var response = await restClient.ExecuteAsync(request);

			if (!response.IsSuccessful)
				throw new ApiException(response.StatusCode, response.StatusDescription, response.Content, response.ErrorMessage);

			return customDeserializationFunction(response);
		}
	}
}
