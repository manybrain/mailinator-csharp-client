using System;
using System.Net;

namespace mailinator_csharp_client.Helpers
{
    public class ApiException : Exception
    {
        public ApiException() { }

        public ApiException(HttpStatusCode httpStatusCode, string statusDescription, string content, string errorMessage)
                      : this(BuildErrorMessage(httpStatusCode, statusDescription, content, errorMessage))
        {
            HttpStatusCode = httpStatusCode;
            StatusDescription = statusDescription;
            Content = content;
            ErrorMessage = errorMessage;
        }

        public ApiException(string message) : this(message, null) { }

        public ApiException(string message, Exception inner) : base(message, inner) { }

        public HttpStatusCode HttpStatusCode { get; }
        public string StatusDescription { get; }
        public string Content { get; }
        public string ErrorMessage { get; }

        private static string BuildErrorMessage(HttpStatusCode httpStatusCode, string statusDescription, string content, string errorMessage)
        {
            return $"StatusCode: {httpStatusCode}. Status description: {statusDescription}. Content: {content}. ErrorMessage: {errorMessage}";
        }
    }
}
