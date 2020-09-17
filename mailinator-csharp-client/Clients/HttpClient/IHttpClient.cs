using RestSharp;
using System;
using System.Threading.Tasks;

namespace mailinator_csharp_client.Clients.HttpClient
{
	public interface IHttpClient
	{
		RestRequest GetRequest(string url, Method method);
		Task<T> ExecuteAsync<T>(RestRequest request);
		Task<T> ExecuteAsync<T>(RestRequest request, Func<IRestResponse, T> customDeserializationFunction);
	}
}
