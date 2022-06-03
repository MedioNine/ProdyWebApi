using System.Text.Json.Serialization;

namespace Prody.Rest.Contracts.Models.Silpo
{
    public class SilpoRequestByCategory
    {
        [JsonPropertyName("categoryID")]
        public int CategoryId { get; set; }

        [JsonPropertyName("filialId")]
        public int FilialId { get; set; }

        [JsonPropertyName("merchandId")]
        public int MerchandId { get; set; }

        [JsonPropertyName("From")]
        public int From { get; set; }

        [JsonPropertyName("To")]
        public int To { get; set; }

        public SilpoRequestByCategory(int categodyId, int filialId, int from, int to)
        {
            CategoryId = categodyId;
            FilialId = filialId;
            To = to;
            From = from;
            MerchandId = 1; // merchant always 1 XD
        }
    }
}
