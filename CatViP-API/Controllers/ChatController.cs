﻿using CatViP_API.Services;
using CatViP_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatViP_API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAll")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IChatService _chatService;

        public ChatController(IAuthService authService, IChatService chatService)
        {
            _authService = authService;
            _chatService = chatService;
        }

        [HttpGet("GetChatUsers"), Authorize(Roles = "Cat Owner,Cat Expert")]
        public async Task<IActionResult> GetChatUsers()
        {
            string authorizationHeader = Request.Headers["Authorization"]!;
            string token = authorizationHeader.Substring("Bearer ".Length);

            var userResult = await _authService.GetUserFromJWTToken(token);

            if (!userResult.IsSuccessful)
            {
                return Unauthorized("invalid token");
            }

            var users = _chatService.GetChatUsers(userResult.Result!.Id);

            return Ok(users);
        }

        [HttpGet("GetChats/{UserId}"), Authorize(Roles = "Cat Owner,Cat Expert")]
        public async Task<IActionResult> GetChats(long UserId)
        {
            string authorizationHeader = Request.Headers["Authorization"]!;
            string token = authorizationHeader.Substring("Bearer ".Length);

            var userResult = await _authService.GetUserFromJWTToken(token);

            if (!userResult.IsSuccessful)
            {
                return Unauthorized("invalid token");
            }

            var users = _chatService.GetChats(userResult.Result!.Id, UserId);

            return Ok(users);
        }
    }
}