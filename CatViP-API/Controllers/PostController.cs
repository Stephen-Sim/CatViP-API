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

        [HttpPost(), Authorize(Roles = "Cat Owner,Expert")]
        public async Task<IActionResult> StorePostAsync([FromHeader]string token, [FromBody]CreatePostDTO createPostDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _authService.GetUserFromJWTToken(token);

            return Ok();
        }
    }
}
