using System.ComponentModel.DataAnnotations;

namespace CatViP_API.DTOs.PostDTOs
{
    public class CreatePostRequestDTO
    {
        [Required]
        public long PostTypeId { get; set; }
        [Required]
        public string Description { get; set; } = null!;
        public ICollection<PostImageDTO> PostImages { get; set; } = new List<PostImageDTO>();
    }

}
