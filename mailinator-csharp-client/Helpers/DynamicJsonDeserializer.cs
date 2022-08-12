using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers;

namespace mailinator_csharp_client.Helpers
{
    public class DynamicJsonSerializer : IRestSerializer
    {
        public ISerializer Serializer => new CustomJsonSerializer();

        public IDeserializer Deserializer => new CustomJsonSerializer();

        public string[] AcceptedContentTypes => ContentType.JsonAccept;

        public SupportsContentType SupportsContentType => (contentType) => contentType == ContentType.Json ? true : false;

        public DataFormat DataFormat { get; } = DataFormat.Json;

        public string Serialize(Parameter parameter) => JsonConvert.SerializeObject(parameter.Value);


        private class CustomJsonSerializer : ISerializer, IDeserializer
        {
            public string RootElement { get; set; }
            public string Namespace { get; set; }
            public string DateFormat { get; set; }
            public string ContentType { get => "application/json"; set { } }

            public T Deserialize<T>(RestResponse response)
            {
                return JsonConvert.DeserializeObject<T>(response.Content,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        Converters = new JsonConverter[] { }
                    });
            }

            public string Serialize(object obj)
            {
                return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    Converters = new JsonConverter[] { }
                });
            }
        }
    }
}
