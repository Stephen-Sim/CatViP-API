using CatViP_API.Data;
using CatViP_API.DTOs;
using CatViP_API.Interfaces;
using CatViP_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CatViP_API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CatViPContext _context;

        public UserRepository(CatViPContext context)
        {
            _context = context;
        }

        public async Task<User> AuthenticateUser(UserLoginDTO userLogin)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == userLogin.Username);

            if (user != null)
            {
                var isValidPassword = BCrypt.Net.BCrypt.Verify(userLogin.Password, user.Password);

                if (isValidPassword)
                {
                    return user;
                }
            }

            return null;
        }

    }
}
