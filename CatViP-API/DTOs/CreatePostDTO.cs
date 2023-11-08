using System.ComponentModel.DataAnnotations;

namespace CatViP_API.DTOs
{
    public class CreatePostDTO
    {
        [Required]
        public long PostTypeId { get; set; }
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public ICollection<PostImageDTO> PostImages { get; set; } = new List<PostImageDTO>();
    }

    public class PostImageDTO
    {
        [Required]
        public byte[] Image { get; set; } = null!;
        [Required]
        public bool IsBloodyContent { get; set; }
    }
}
