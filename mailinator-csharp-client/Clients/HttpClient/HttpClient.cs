using mailinator_csharp_client.Helpers;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace mailinator_csharp_client.Clients.HttpClient
{
    public class HttpClient : IHttpClient
	{
		private const int TIMEOUT_MS = 125 * 1000;
		private readonly RestClient restClient;

        public HttpClient(string baseUri)
        {
            // Set UserAgent header with SDK version automatically
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyVersion = assembly.GetName().Version;

            // Extract major, minor, and build version numbers
            var sdkVersion = $"{assemblyVersion.Major}.{assemblyVersion.Minor}.{assemblyVersion.Build}";

			var userAgent = $"Mailinator SDK - .NET V{sdkVersion}";

            var options = new RestClientOptions()
            {
                Timeout = TimeSpan.FromMilliseconds(TIMEOUT_MS),
				UserAgent = userAgent,
                BaseUrl = new Uri(baseUri)
            };

            restClient = new RestClient(options, configureSerialization: s => s.UseSerializer(() => new DynamicJsonSerializer()));
        }

        public HttpClient(string apiKey, string baseUri) : this(baseUri)
		{
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
