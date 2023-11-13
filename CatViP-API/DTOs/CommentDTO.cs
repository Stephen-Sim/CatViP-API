namespace CatViP_API.DTOs
{
    public class CommentDTO
    {
        public long Id { get; set; }

        public string Description { get; set; } = null!;

        public DateTime DateTime { get; set; }

        public string Username { get; set; } = null!;
    }
}
