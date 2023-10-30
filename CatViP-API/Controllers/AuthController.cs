﻿using CatViP_API.DTOs;
using CatViP_API.Repositories.Interfaces;
using CatViP_API.Services;
using CatViP_API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Login(UserLoginDTO userLogin)
        {
            var user = await _authService.Login(userLogin);

            if (user == null)
                return Unauthorized();

            // get token
            var token = await _authService.CreateToken(user, userLogin.IsMobileLogin);

            return Ok(token);
        }
    }
}
