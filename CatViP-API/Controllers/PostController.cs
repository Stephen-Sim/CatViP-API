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
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("GetPostTypes"), Authorize(Roles = "Cat Owner,Expert")]
        public IActionResult GetPostTypes()
        {
            var result = _postService.GetPostTypes();
            return Ok(result);
        }
    }
}
