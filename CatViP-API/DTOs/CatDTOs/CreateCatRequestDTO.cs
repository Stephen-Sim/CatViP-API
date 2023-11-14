﻿using System.ComponentModel.DataAnnotations;

namespace CatViP_API.DTOs.CatDTOs
{
    public class CreateCatRequestDTO
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public bool Gender { get; set; }
        public byte[]? ProfileImage { get; set; }
    }
}
