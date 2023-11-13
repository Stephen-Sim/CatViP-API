﻿using System.ComponentModel.DataAnnotations;

namespace CatViP_API.DTOs
{
    public class PostImageDTO
    {
        [Required]
        public byte[] Image { get; set; } = null!;
        [Required]
        public bool IsBloodyContent { get; set; }
    }
}
