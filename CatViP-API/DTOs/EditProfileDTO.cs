using System.ComponentModel.DataAnnotations;

namespace CatViP_API.DTOs
{
    public class EditProfileDTO
    {
        [Required]
        public string FullName { get; set; } = null!;
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public bool Gender { get; set; }
        [Required]
        public string Address { get; set; } = null!;
        [Required]
        public decimal Longitude { get; set; }
        [Required]
        public decimal Latitude { get; set; }
        [Required]
        public byte[] ProfileImage { get; set; } = null!;
    }
}
