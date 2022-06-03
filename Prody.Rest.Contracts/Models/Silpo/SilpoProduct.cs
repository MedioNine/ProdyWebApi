using System.Text.Json.Serialization;

namespace Prody.Rest.Contracts.Models.Silpo
{
    public class SilpoProduct
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("mainImage")]
        public string Image { get; set; }

        [JsonPropertyName("price")]
        public float Price { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; }
    }
}
