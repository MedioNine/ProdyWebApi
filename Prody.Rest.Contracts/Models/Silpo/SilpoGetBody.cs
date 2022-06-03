using System.Text.Json.Serialization;

namespace Prody.Rest.Contracts.Models.Silpo
{
    public class SilpoGetBody<T> where T : class
    {
        [JsonPropertyName("data")]
        public T Data { get; set; }

        [JsonPropertyName("Method")]
        public string Method { get; set; }

        public SilpoGetBody(T data, string method)
        {
            Data = data;
            Method = method;
        }
    }
}
