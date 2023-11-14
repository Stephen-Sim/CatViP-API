namespace CatViP_API.DTOs.PostDTOs
{
    public class PostDTO
    {
        public long Id { get; set; }

        public string Description { get; set; } = null!;

        public DateTime DateTime { get; set; }

        public long UserId { get; set; }

        public long PostTypeId { get; set; }

        public int LikeCount { get; set; }

        public int CurrentUserAction { get; set; }

        public int CommentCount { get; set; }

        public int DislikeCount { get; set; }
        public ICollection<PostImageDTO> PostImages { get; set; } = new List<PostImageDTO>();
    }
}
