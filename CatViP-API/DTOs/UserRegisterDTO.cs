using System.ComponentModel.DataAnnotations;

namespace CatViP_API.DTOs
{
    public class UserRegisterDTO
    {
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public string FullName { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public bool Gender { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public long RoleId { get; set; }
    }
}
