using System.ComponentModel.DataAnnotations;

namespace CatViP_API.DTOs
{
    public class CommentRequestDTO
    {
        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public long PostId { get; set; }
    }
}
