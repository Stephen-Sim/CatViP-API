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

            var user = await _authService.Login(userLogin);

            if (user == null)
                return Unauthorized();

            // get token
            var token = await _authService.CreateToken(user);

            return Ok(token);
        }

        [HttpPut("refresh")]
        public async Task<IActionResult> RefreshToken([FromHeader]string token)
        {
            var user = await _authService.GetUserFromJWTToken(token);

            var res = _authService.VerifyToken(token, user);

            if (!res.IsSuccessful)
            {
                return Unauthorized(res.ErrorMessage);
            }

            var newToken = await _authService.CreateToken(user);

            return Ok(newToken);
        }

        [HttpDelete("logout")]
        public async Task<IActionResult> Logout([FromHeader]string token)
        {
            var user = await _authService.GetUserFromJWTToken(token);

            var res = _authService.VerifyToken(token, user);

            if (!res.IsSuccessful)
            {
                return Unauthorized(res.ErrorMessage);
            }

            await _authService.DeleteToken(user.Id);

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

            if (user == null)
            {
                return BadRequest("fail to register");
            }

            var newToken = await _authService.CreateToken(user);

            return Ok(newToken);
        }
    }
}
