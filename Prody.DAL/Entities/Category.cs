using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prody.DAL.Entities
{
    public class Category
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]

        public string Name { get; set; }

        public int Items { get; set; }

        [InverseProperty("Category")]
        public List<Product> Products { get; set; }
    }
}
