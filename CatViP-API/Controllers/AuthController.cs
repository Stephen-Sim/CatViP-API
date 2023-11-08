using CatViP_API.DTOs;
using CatViP_API.Models;
using CatViP_API.Repositories.Interfaces;
using CatViP_API.Services;
using CatViP_API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace CatViP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserLoginDTO userLogin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lgoinResult = await _authService.Login(userLogin);

            if (!lgoinResult.IsSuccessful)
                return Unauthorized();

            // get token
            var tokenResult = await _authService.CreateToken(lgoinResult.Result!);

            return Ok(tokenResult.Result);
        }

        [HttpPut("refresh")]
        public async Task<IActionResult> RefreshToken([FromHeader]string token)
        {
            var userResult = await _authService.GetUserFromJWTToken(token);

            if (!userResult.IsSuccessful)
                return Unauthorized();

            var verifyTokenResult = _authService.VerifyToken(token, userResult.Result!);

            if (!verifyTokenResult.IsSuccessful)
            {
                return Unauthorized(verifyTokenResult.ErrorMessage);
            }

            var newTokenResult = await _authService.CreateToken(userResult.Result!);

            return Ok(newTokenResult.Result);
        }

        [HttpDelete("logout")]
        public async Task<IActionResult> Logout([FromHeader]string token)
        {
            var userResult = await _authService.GetUserFromJWTToken(token);

            if (!userResult.IsSuccessful)
                return Unauthorized();

            var res = _authService.VerifyToken(token, userResult.Result!);

            if (!res.IsSuccessful)
                return Unauthorized(res.ErrorMessage);

            await _authService.DeleteToken(userResult.Result!.Id);

            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserRegisterDTO userRegisterDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resResult = _authService.ValidateRegisterRoleId(userRegisterDTO.RoleId);

            if (!resResult.IsSuccessful)
            {
                return BadRequest(resResult.ErrorMessage);
            }

            resResult = _authService.ValidateUsernameAndEmail(userRegisterDTO);

            if (!resResult.IsSuccessful)
            { 
                return Conflict(resResult.ErrorMessage);
            }

            var user = await _authService.StoreUser(userRegisterDTO);

            if (!user.IsSuccessful)
            {
                return BadRequest("fail to register");
            }

            var newTokenResult = await _authService.CreateToken(user.Result!);

            return Ok(newTokenResult.Result);
        }

        /*public IActionResult Get(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || !email.Contains("@"))
                {
                    return BadRequest("Invalid email address");
                }

                var result = _authService.ValidateEmail(email);

                if (!result.IsSuccessful)
                {
                    return BadRequest("Invalid Email");
                }

                var link = _authService.GenerateForgotPasswordLink(email);

                return Ok(link);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }*/

    }
}
