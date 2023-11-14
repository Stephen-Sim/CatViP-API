using CatViP_API.DTOs.PostDTOs;
using CatViP_API.Models;

namespace CatViP_API.Repositories.Interfaces
{
    public interface IPostRepository
    {
        ICollection<PostType> GetPostTypes();
        ICollection<Post> GetPosts();
        Task<bool> StorePost(Post post);
        int GetPostDisLikeCount(long postId);
        int GetPostLikeCount(long postId);
        int GetCurrentUserStatusOnPost(long userId, long postId);
        ICollection<Comment> GetPostComments(long postId);
        int GetPostCommentCount(long postId);
        ICollection<PostImage> GetPostImages(long postId);
        Task<bool> ActPost(long userId, PostActionRequestDTO postActionDTO);
        Task<bool> CommentPost(long userId, CommentRequestDTO commentRequestDTO);
        ICollection<Post> GetOwnPosts(long userId);
    }
}
