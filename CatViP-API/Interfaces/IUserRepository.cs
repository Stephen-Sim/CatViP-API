using CatViP_API.DTOs;
using CatViP_API.Models;

namespace CatViP_API.Interfaces
{
    public interface IUserRepository
    {
        Task<User> AuthenticateUser(UserLoginDTO user);
    }
}
