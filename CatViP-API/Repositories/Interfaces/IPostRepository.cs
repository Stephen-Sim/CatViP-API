using CatViP_API.Models;

namespace CatViP_API.Repositories.Interfaces
{
    public interface IPostRepository
    {
        IEnumerable<PostType> GetPostTypes();
    }
}
