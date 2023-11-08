using Azure.Core;
using CatViP_API.DTOs;
using CatViP_API.Models;
using CatViP_API.Repositories;
using CatViP_API.Repositories.Interfaces;
using CatViP_API.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Net;
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

        public async Task DeleteToken(long userId)
        {
            await _userRepository.DeleteUserToken(userId);
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

        public ResponseResult ValidateUsernameAndEmail(UserRegisterDTO userRegisterDTO)
        {
            var resResult = new ResponseResult();
            resResult.IsSuccessful = true;

            if (_userRepository.CheckIfUsernameExist(userRegisterDTO.Username))
            {
                resResult.IsSuccessful = false;
                resResult.ErrorMessage = "Username is already registered in the system.";
            }
            else if (_userRepository.CheckIfEmailExist(userRegisterDTO.Email))
            {
                resResult.IsSuccessful = false;
                resResult.ErrorMessage = "Email is already registered in the system.";
            }

            return resResult;
        }

        public ResponseResult ValidateEmail(string email)
        {
            var resResult = new ResponseResult();
            resResult.IsSuccessful = true;
            
            if (_userRepository.CheckIfEmailExist(email))
            {
                resResult.IsSuccessful = false;
                resResult.ErrorMessage = "Email is already registered in the system.";
            }

            return resResult;
        }

        public ResponseResult ValidateRegisterRoleId(long RoleId)
        {
            var resResult = new ResponseResult();
            resResult.IsSuccessful = true;

            if (RoleId != 2 && RoleId != 4)
            {
                resResult.IsSuccessful = false;
                resResult.ErrorMessage = "Invalid Role Id";
            }

            return resResult;
        }

        public async Task<User?> StoreUser(UserRegisterDTO userRegisterDTO)
        {
            var user = await _userRepository.StoreUser(userRegisterDTO);
            return user;
        }

        public string GenerateForgotPasswordLink(string email)
        {
            var emailBytes = System.Text.Encoding.UTF8.GetBytes(email);

            var encryptedEmail = Convert.ToBase64String(emailBytes);

            var link = $"https://www.yourwebsite.com/forgotpassword?email={encryptedEmail}";

            return link;
        }

        public async Task SendEmail(string email, string link)
        {
            var smtpClient = new SmtpClient("smtp.example.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("username@example.com", "password"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("username@example.com"),
                Subject = "Forgot Password",
                Body = $"<p>Please click the following link to reset your password:</p><a href='{link}'>Reset Password</a>",
                IsBodyHtml = true,
            };

            mailMessage.To.Add(email);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
