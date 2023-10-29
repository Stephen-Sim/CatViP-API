using CatViP_API.DTOs;
using CatViP_API.Models;

namespace CatViP_API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> AuthenticateUser(UserLoginDTO userLogin);
    }
}
