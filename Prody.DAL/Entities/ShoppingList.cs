using System.ComponentModel.DataAnnotations;

namespace Prody.DAL.Entities
{
    public class ShoppingList
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ProductList { get; set; }
    }
}
