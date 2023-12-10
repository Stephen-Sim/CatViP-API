using System.ComponentModel.DataAnnotations;

namespace CatViP_API.DTOs.ProductDTOs
{
    public class ProductImageDTO
    {
        [Required]
        public byte[] Image { get; set; } = null!;
    }
}
