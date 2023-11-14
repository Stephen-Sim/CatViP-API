using AutoMapper;
using CatViP_API.DTOs.CatDTOs;
using CatViP_API.DTOs.ExpertDTOs;
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
            CreateMap<MentionedCat, MentionedCatDTO>()
                .ForMember(dest => dest.CatName, opt => opt.MapFrom(src => src.Cat.Name));
            CreateMap<Cat, CatDTO>();
            CreateMap<ExpertApplication, ExpertApplicationDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Name));
        }
    }
}
