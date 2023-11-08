using AutoMapper;
using CatViP_API.DTOs;
using CatViP_API.Models;

namespace CatViP_API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<PostType, PostTypeDTO>();
        }
    }
}
