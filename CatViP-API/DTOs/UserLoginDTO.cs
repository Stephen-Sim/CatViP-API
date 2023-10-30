namespace CatViP_API.DTOs
{
    public class UserLoginDTO
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool IsMobileLogin { get; set; }
    }
}
