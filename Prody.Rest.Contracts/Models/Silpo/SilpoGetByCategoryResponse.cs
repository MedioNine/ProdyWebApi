using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Prody.Rest.Contracts.Models.Silpo
{
    public class SilpoGetByCategoryResponse
    {
        [JsonPropertyName("itemsCount")]
        public int Count { get; set; }

        [JsonPropertyName("items")]
        public List<SilpoProduct> Items { get; set; }
    }
}
