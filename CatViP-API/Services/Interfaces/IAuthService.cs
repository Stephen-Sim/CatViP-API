using CatViP_API.DTOs;

namespace CatViP_API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> Login(UserLoginDTO userLoginDTO);
        Task<string> CreateToken();
        Task<string> RefreshToken(string token);
        Task DeleteToken(string token);
    }
}
