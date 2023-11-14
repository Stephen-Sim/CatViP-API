using CatViP_API.DTOs.PostDTOs;
using CatViP_API.Models;

namespace CatViP_API.Services.Interfaces
{
    public interface IPostService
    {
        ICollection<PostDTO> GetPosts(User currentUser);
        ICollection<PostDTO> GetOwnPosts(User currentUser);
        ICollection<PostDTO> GetPostsByCatId(long currentUserId, long catId);
        ICollection<PostTypeDTO> GetPostTypes(bool isExpert);
        Task<ResponseResult> CreatePost(User user, PostRequestDTO createPostDTO);
        Task<ResponseResult> ActionPost(User user, PostActionRequestDTO postActionDTO);
        Task<ResponseResult> CommentPost(User user, CommentRequestDTO commentRequestDTO);
        ICollection<CommentDTO> GetPostComments(int postId);
    }
}
