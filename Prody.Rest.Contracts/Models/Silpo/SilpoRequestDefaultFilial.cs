using System.Text.Json.Serialization;

namespace Prody.Rest.Contracts.Models.Silpo
{
    public class SilpoRequestDefaultFilial
    {
        [JsonPropertyName("merchandId")]
        public int MerchantId { get; set; }

        public SilpoRequestDefaultFilial()
        {
            MerchantId = 1; // magic number, I know
        }

        public SilpoRequestDefaultFilial(int merchantId)
        {
            MerchantId = merchantId;
        }
    }
}
