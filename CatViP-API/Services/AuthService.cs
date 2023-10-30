﻿using Azure.Core;
using CatViP_API.DTOs;
using CatViP_API.Models;
using CatViP_API.Repositories;
using CatViP_API.Repositories.Interfaces;
using CatViP_API.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CatViP_API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            this._userRepository = userRepository;
            this._configuration = configuration;
        }

        public async Task<User?> Login(UserLoginDTO userLoginDTO)
        {
            var user = await _userRepository.AuthenticateUser(userLoginDTO);

            if (user == null)
            {
                return null;
            }

            return user;
        }

        public async Task<string> CreateToken(User user)
        {
            var roleName = _userRepository.GetUserRoleName(user);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, roleName)
            };

            var values = new
            {
                TokenCreated = DateTime.Now,
                TokenExpires = DateTime.Now.AddDays(7)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(claims: claims, expires: values.TokenExpires, signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            await _userRepository.UpdateUserToken(user.Id, jwt, values.TokenCreated, values.TokenExpires);

            return await Task.FromResult(jwt);
        }

        public Task DeleteToken(string token)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetUserFromJWTToken(string token)
        {
            try
            {
                var jwt = new JwtSecurityToken(token);
                long userId = long.Parse(jwt.Claims.First(c => c.Type == _configuration.GetSection("Claims:Sid").Value).Value);
                var user = await _userRepository.GetUserById(userId);
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ResponseResult VerifyToken(string token, User user)
        {
            var resResult = new ResponseResult();

            if (user.RememberToken != token)
            {
                resResult.IsSuccessful = false;
                resResult.ErrorMessage = "Invalid Token.";
            }
            else if (user.TokenExpires < DateTime.Now)
            {
                resResult.IsSuccessful = false;
                resResult.ErrorMessage = "Token expired.";
            }
            else
            {
                resResult.IsSuccessful = true;
            }

            return resResult;
        }
    }
}
