﻿using mailinator_csharp_client.Helpers;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace mailinator_csharp_client.Clients.HttpClient
{
    public class HttpClient : IHttpClient
	{
		private const int TIMEOUT_MS = 100000;
		private readonly RestClient restClient;

        public HttpClient(string baseUri)
        {
            var options = new RestClientOptions(baseUri)
            {
                Timeout = TimeSpan.FromMilliseconds(TIMEOUT_MS)
            };

            restClient = new RestClient(options, configureSerialization: s => s.UseSerializer(() => new DynamicJsonSerializer()));

            // Set UserAgent header with SDK version automatically
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyVersion = assembly.GetName().Version;

            // Extract major, minor, and build version numbers
            var sdkVersion = $"{assemblyVersion.Major}.{assemblyVersion.Minor}.{assemblyVersion.Build}";

            restClient.AddDefaultHeader("User-Agent", $"Mailinator SDK - .NET V{sdkVersion}");
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
