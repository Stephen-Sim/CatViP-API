using CatViP_API.Data;
using CatViP_API.DTOs;
using CatViP_API.Models;
using CatViP_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
            var user = await _context.Users.Include(x => x.UserRoles).ThenInclude(x => x.Role).FirstOrDefaultAsync(x => x.Username == userLogin.Username);

            if (user != null)
            {
                var isValidPassword = BCrypt.Net.BCrypt.Verify(userLogin.Password, user.Password);

                if (isValidPassword)
                {
                    var roleMap = new Dictionary<long, bool>
                    {
                        {1, false},
                        {2, true},
                        {3, true},
                        {4, false}
                    };

                    // Check if any of the user's roles match the IsMobileLogin value
                    var hasMatchingRole = user.UserRoles.Any(role => roleMap.ContainsKey(role.RoleId) && roleMap[role.RoleId] == userLogin.IsMobileLogin);

                    if (hasMatchingRole)
                    {
                        return user;
                    }
                }
            }

            return null;
        }

        public string GetMoibleUserTopRole(User user)
        {
            var rolePriorities = new List<long> { 3, 2 };

            var validRoles = user.UserRoles.Where(r => rolePriorities.Contains(r.RoleId));

            var topRole = validRoles.OrderBy(r => rolePriorities.IndexOf(r.RoleId)).FirstOrDefault();

            return topRole?.Role.Name;
        }

        public string GetWebUserRole(User user)
        {
            return user.UserRoles.FirstOrDefault()?.Role.Name;
        }

        public async Task UpdateUserToken(long userId, string JWT, DateTime TokenCreated, DateTime TokenExpires)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(x => x.Id == userId);
                
                if (user != null)
                {
                    user.RememberToken = JWT;
                    user.TokenCreated = TokenCreated;
                    user.TokenExpires = TokenExpires;
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception err)
            {
                // Handle the exception, log it, or rethrow as needed.
                Console.WriteLine(err.Message);
                throw; 
            }
        }
    }
}
