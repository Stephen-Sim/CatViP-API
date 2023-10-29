using CatViP_API.DTOs;
using CatViP_API.Repositories;
using CatViP_API.Repositories.Interfaces;
using CatViP_API.Services.Interfaces;

namespace CatViP_API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(UserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<string> Login(UserLoginDTO userLoginDTO)
        {
            var res = await _userRepository.AuthenticateUser(userLoginDTO);

            if (res == null)
            {
                return string.Empty;
            }

            return "";
        }

        public Task<string> CreateToken()
        {
            throw new NotImplementedException();
        }

        public Task DeleteToken(string token)
        {
            throw new NotImplementedException();
        }

        public Task<string> RefreshToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
