using CatViP_API.DTOs;
using CatViP_API.Models;

namespace CatViP_API.Services.Interfaces
{
    public interface IPostService
    {
        ICollection<PostTypeDTO> GetPostTypes(bool isExpert);
        Task<ResponseResult> CreatePost(User user, CreatePostDTO createPostDTO);
    }
}
