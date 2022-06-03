using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Prody.Rest.Contracts.Models.Silpo
{
    public class SilpoCategories
    {
        [JsonPropertyName("tree")]
        public List<SilpoCategory> Categories { get; set; }
    }
}
