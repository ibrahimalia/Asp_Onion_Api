using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Data.DTOs
{
    public class ProductDTO
    {
        [Required]
         public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }
    }
}