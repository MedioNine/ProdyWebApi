using System.Text.Json.Serialization;

namespace Prody.Rest.Contracts.Models.Silpo
{
    public class SilpoDefaultResponse<T> where T : class
    {
        [JsonPropertyName("data")]
        public T Data { get; set; }
    }
}
