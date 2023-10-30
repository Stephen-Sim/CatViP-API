using CatViP_API.DTOs;
using CatViP_API.Models;

namespace CatViP_API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<User?> Login(UserLoginDTO userLoginDTO);
        Task<string> CreateToken(User user);
        Task DeleteToken(long userId);
        Task<User?> GetUserFromJWTToken(string token);
        ResponseResult VerifyToken(string token, User userId);
        ResponseResult ValidateUsernameAndEmail(UserRegisterDTO userRegisterDTO);
        ResponseResult ValidateRegisterRoleId(long RoleId);
        Task<User?> StoreUser(UserRegisterDTO userRegisterDTO);
    }
}
