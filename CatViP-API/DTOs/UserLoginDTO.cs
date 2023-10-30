﻿using System.ComponentModel.DataAnnotations;

namespace CatViP_API.DTOs
{
    public class UserLoginDTO
    {
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public bool IsMobileLogin { get; set; }
    }
}
