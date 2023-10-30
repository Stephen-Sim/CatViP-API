using CatViP_API.DTOs;
using CatViP_API.Models;

namespace CatViP_API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<User> Login(UserLoginDTO userLoginDTO);
        Task<string> CreateToken(User user, bool IsMobleLogin);
        Task DeleteToken(string token);
        Task<string> RefreshToken(string token);
    }
}
