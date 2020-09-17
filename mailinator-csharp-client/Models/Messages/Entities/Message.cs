using Newtonsoft.Json;
using System.Collections.Generic;

namespace mailinator_csharp_client.Models.Messages.Entities
{
    public class Message
    {
        [JsonProperty("subject")]
        public string Subject;
        [JsonProperty("from")]
        public string From;
        [JsonProperty("to")]
        public string To;
        [JsonProperty("id")]
        public string Id;
        [JsonProperty("time")]
        public long Time;
        [JsonProperty("seconds_ago")]
        public long SecondsAgo;
        [JsonProperty("domain")]
        public string Domain;


        [JsonProperty("is_first_exchange")]
        public bool IsFirstExchange;
        [JsonProperty("fromfull")]
        public string Fromfull;
        [JsonProperty("headers")]
        public Dictionary<string, object> Headers;
        [JsonProperty("parts")]
        public List<Part> Parts;
        [JsonProperty("origfrom")]
        public string Origfrom;
        [JsonProperty("mrid")]
        public string Mrid;
        [JsonProperty("size")]
        public int Size;
        [JsonProperty("stream")]
        public string Stream;
        [JsonProperty("msg_type")]
        public string MsgType;
        [JsonProperty("source")]
        public string Source;
        [JsonProperty("text")]
        public string Text;
    }
}
