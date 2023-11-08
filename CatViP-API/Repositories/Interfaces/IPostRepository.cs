using CatViP_API.DTOs;
using CatViP_API.Models;

namespace CatViP_API.Repositories.Interfaces
{
    public interface IPostRepository
    {
        ICollection<PostType> GetPostTypes();
        Task<bool> StorePost(Post post);
    }
}
