using CatViP_API.DTOs;
using CatViP_API.Models;

namespace CatViP_API.Services.Interfaces
{
    public interface IPostService
    {
        IEnumerable<PostTypeDTO> GetPostTypes();
    }
}
