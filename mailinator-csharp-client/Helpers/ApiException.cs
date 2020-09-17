using System;

namespace mailinator_csharp_client.Helpers
{
    public class ApiException : Exception
    {
        public ApiException() { }

        public ApiException(string message) : this(message, null) { }

        public ApiException(string message, Exception inner) : base(message, inner) { }
    }
}
