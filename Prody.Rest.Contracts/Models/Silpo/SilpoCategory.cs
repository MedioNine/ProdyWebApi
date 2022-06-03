using System.Text.Json.Serialization;

namespace Prody.Rest.Contracts.Models.Silpo
{
    public class SilpoCategory
    {
        [JsonPropertyName("itemsCount")]
        public int Items { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("parentId")]
        public int? ParentId { get; set; }
    }
}
