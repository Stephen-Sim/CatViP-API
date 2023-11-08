using CatViP_API.DTOs;
using CatViP_API.Services;
using CatViP_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatViP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IPostService _postService;

        public PostController(IAuthService authService, IPostService postService)
        {
            _authService = authService;
            _postService = postService;
        }

        [HttpGet("GetPostTypes"), Authorize(Roles = "Cat Owner,Expert")]
        public IActionResult GetPostTypes()
        {
            var result = _postService.GetPostTypes();
            return Ok(result);
        }

        [HttpPost("CreatePost"), Authorize(Roles = "Cat Owner,Expert")]
        public async Task<IActionResult> CreatePost([FromHeader]string token, [FromBody]CreatePostDTO createPostDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userResult = await _authService.GetUserFromJWTToken(token);

            // check if user is expert
            if (!userResult.IsSuccessful)
            {
                return Unauthorized("invalid token");
            }

            var createPostResult = await _postService.CreatePost(userResult.Result!, createPostDTO);

            if (!createPostResult.IsSuccessful)
            {
                return BadRequest(createPostResult.ErrorMessage);
            }

            return Ok();
        }
    }
}
