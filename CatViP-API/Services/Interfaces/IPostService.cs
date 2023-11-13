using CatViP_API.DTOs;
using CatViP_API.Models;

namespace CatViP_API.Services.Interfaces
{
    public interface IPostService
    {
        ICollection<PostDTO> GetPosts(User user);
        ICollection<PostTypeDTO> GetPostTypes(bool isExpert);
        Task<ResponseResult> CreatePost(User user, CreatePostRequestDTO createPostDTO);
        Task<ResponseResult> ActionPost(User user, PostActionRequestDTO postActionDTO);
        Task<ResponseResult> CommentPost(User user, CommentRequestDTO commentRequestDTO);
        ICollection<CommentDTO> GetPostComments(int postId);
    }
}
