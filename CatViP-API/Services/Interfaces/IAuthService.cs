using CatViP_API.DTOs;
using CatViP_API.Models;

namespace CatViP_API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseResult<User?>> Login(UserLoginDTO userLoginDTO);
        Task<ResponseResult<User?>> GetUser(long userId);
        Task<ResponseResult<string>> CreateToken(User user);
        Task<ResponseResult> DeleteToken(long userId);
        Task<ResponseResult<User?>> GetUserFromJWTToken(string token);
        ResponseResult VerifyToken(string token, User userId);
        ResponseResult ValidateUsernameAndEmail(UserRegisterDTO userRegisterDTO);
        ResponseResult ValidateRegisterRoleId(long RoleId);
        Task<ResponseResult<User?>> StoreUser(UserRegisterDTO userRegisterDTO);
        ResponseResult ValidateEmail(string email);
        // ResponseResult<string> GenerateForgotPasswordLink(string email);
    }
}
