using CatViP_API.DTOs;
using CatViP_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatViP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO userLogin)
        {
            var authenticatedUser = await _userRepository.AuthenticateUser(userLogin);

            if (authenticatedUser == null)
                return Unauthorized();

            return Ok(new { Token = "..." });
        }
    }
}
