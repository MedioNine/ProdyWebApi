using System.Text.Json.Serialization;

namespace Prody.Rest.Contracts.Models.Silpo
{
    public class SilpoDefaultSettings
    {
        [JsonPropertyName("filialId")]
        public int FilialId { get; set; }

        [JsonPropertyName("deliveryType")]
        public int DeliveryType { get; set; }
    }
}
