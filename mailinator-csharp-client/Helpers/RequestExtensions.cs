using RestSharp;

namespace mailinator_csharp_client.Helpers
{
    public static class RequestExtensions
    {
        public static void AddSafeParameter(this RestRequest request, string parameter, string value)
        {
            if (!string.IsNullOrEmpty(parameter) && value != null)
            {
                request.AddParameter(parameter, value);
            }
        }

        public static void AddSafeQueryParameter(this RestRequest request, string parameter, string value)
        {
            if (!string.IsNullOrEmpty(parameter) && value != null)
            {
                request.AddQueryParameter(parameter, value);
            }
        }
    }
}
