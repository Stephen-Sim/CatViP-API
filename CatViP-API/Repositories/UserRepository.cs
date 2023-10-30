using Azure.Core;
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

        public async Task<User?> AuthenticateUser(UserLoginDTO userLogin)
        {
            var user = await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Username == userLogin.Username);

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

                    if (roleMap.ContainsKey(user.RoleId) && roleMap[user.RoleId] == userLogin.IsMobileLogin)
                    {
                        return user;
                    }
                }
            }

            return null;
        }

        public async Task DeleteUserToken(long userId)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(x => x.Id == userId);

                if (user != null)
                {
                    user.RememberToken = null;
                    user.TokenCreated = null;
                    user.TokenExpires = null;
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

        public async Task<User?> GetUserById(long userId)
        {
            return await _context.Users.Include(x => x.Role).FirstOrDefaultAsync();
        }

        public string GetUserRoleName(User user)
        {
            return _context.Roles.FirstOrDefault(x => x.Id == user.RoleId).Name;
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

        public bool CheckIfUsernameExist(string username)
        {
            return _context.Users.Any(x => x.Username == username);
        }

        public bool CheckIfEmailExist(string email)
        {
            return _context.Users.Any(x => x.Email == email);
        }
        
        public async Task<User?> StoreUser(UserRegisterDTO userRegisterDTO)
        {
            try
            {
                var user = new User();
                user.Username = userRegisterDTO.Username;
                user.FullName = userRegisterDTO.FullName;
                user.Email = userRegisterDTO.Email;
                user.Password = BCrypt.Net.BCrypt.HashPassword(userRegisterDTO.Password);
                user.Gender = userRegisterDTO.Gender;
                user.DateOfBirth = userRegisterDTO.DateOfBirth;
                user.RoleId = userRegisterDTO.RoleId;

                _context.Add(user);
                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception err)
            {
                await Console.Out.WriteLineAsync(err.Message);
                return null;
            }
        }
    }
}
