using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prody.DAL.Entities
{
    public class Price
    {
        public int Id { get; set; }

        public float Value { get; set; }

        [Required]
        [MaxLength(100)]
        public string Seller { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]

        public Product Product { get; set; }
    }
}
