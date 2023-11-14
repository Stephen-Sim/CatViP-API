using AutoMapper;
using CatViP_API.DTOs.CatDTOs;
using CatViP_API.DTOs.PostDTOs;
using CatViP_API.Models;

namespace CatViP_API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<PostType, PostTypeDTO>();
            CreateMap<Post, PostDTO>();
            CreateMap<PostImage, PostImageDTO>();
            CreateMap<Comment, CommentDTO>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.FullName));

            CreateMap<Cat, CatDTO>();
        }
    }
}
