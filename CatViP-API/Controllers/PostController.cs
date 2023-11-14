using CatViP_API.DTOs.PostDTOs;
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

        [HttpGet("GetOwnPosts"), Authorize(Roles = "Cat Owner,Cat Expert")]
        public async Task<IActionResult> GetOwnPosts()
        {
            string authorizationHeader = Request.Headers["Authorization"]!;
            string token = authorizationHeader.Substring("Bearer ".Length);

            var userResult = await _authService.GetUserFromJWTToken(token);

            if (!userResult.IsSuccessful)
            {
                return Unauthorized("invalid token");
            }

            var posts = _postService.GetPosts(userResult.Result!);

            return Ok(posts);
        }

        [HttpGet("GetCatPosts/{catId}"), Authorize(Roles = "Cat Owner,Cat Expert")]
        public async Task<IActionResult> GetPostByCat(long catId)
        {
            string authorizationHeader = Request.Headers["Authorization"]!;
            string token = authorizationHeader.Substring("Bearer ".Length);

            var userResult = await _authService.GetUserFromJWTToken(token);

            if (!userResult.IsSuccessful)
            {
                return Unauthorized("invalid token");
            }

            var posts = _postService.GetPostsByCatId(userResult.Result!.Id, catId);
            return Ok(posts);
        }

        [HttpGet("GetPosts"), Authorize(Roles = "Cat Owner,Cat Expert")]
        public async Task<IActionResult> GetPosts()
        {
            string authorizationHeader = Request.Headers["Authorization"]!;
            string token = authorizationHeader.Substring("Bearer ".Length);

            var userResult = await _authService.GetUserFromJWTToken(token);

            if (!userResult.IsSuccessful)
            {
                return Unauthorized("invalid token");
            }

            var posts = _postService.GetOwnPosts(userResult.Result!);

            return Ok(posts);
        }

        [HttpGet("GetPostComments"), Authorize(Roles = "Cat Owner,Cat Expert")]
        public async Task<IActionResult> GetPostComments(int postId)
        {
            string authorizationHeader = Request.Headers["Authorization"]!;
            string token = authorizationHeader.Substring("Bearer ".Length);

            var userResult = await _authService.GetUserFromJWTToken(token);

            if (!userResult.IsSuccessful)
            {
                return Unauthorized("invalid token");
            }

            var postComments = _postService.GetPostComments(postId);

            return Ok(postComments);
        }

        [HttpPut("ActPost"), Authorize(Roles = "Cat Owner,Cat Expert")]
        public async Task<IActionResult> ActPost([FromBody] PostActionRequestDTO postActionDTO)
        {
            string authorizationHeader = Request.Headers["Authorization"]!;
            string token = authorizationHeader.Substring("Bearer ".Length);

            var userResult = await _authService.GetUserFromJWTToken(token);

            if (!userResult.IsSuccessful)
            {
                return Unauthorized("invalid token");
            }

            var postActRes = await _postService.ActPost(userResult.Result!, postActionDTO);

            if (!postActRes.IsSuccessful)
            {
                return BadRequest("fail to act the post");
            }

            return Ok();
        }

        [HttpPost("CreateComment"), Authorize(Roles = "Cat Owner,Cat Expert")]
        public async Task<IActionResult> CreateComment([FromBody] CommentRequestDTO commentRequestDTO)
        {
            string authorizationHeader = Request.Headers["Authorization"]!;
            string token = authorizationHeader.Substring("Bearer ".Length);

            var userResult = await _authService.GetUserFromJWTToken(token);

            if (!userResult.IsSuccessful)
            {
                return Unauthorized("invalid token");
            }

            var postActRes = await _postService.CommentPost(userResult.Result!, commentRequestDTO);

            if (!postActRes.IsSuccessful)
            {
                return BadRequest("fail to comment");
            }

            return Ok();
        }

        [HttpGet("GetPostTypes"), Authorize(Roles = "Cat Owner,Cat Expert")]
        public async Task<IActionResult> GetPostTypes()
        {
            string authorizationHeader = Request.Headers["Authorization"]!;
            string token = authorizationHeader.Substring("Bearer ".Length);

            var userResult = await _authService.GetUserFromJWTToken(token);

            if (!userResult.IsSuccessful)
            {
                return Unauthorized("invalid token");
            }

            var isExpert = User.IsInRole("Cat Expert");
            var result = _postService.GetPostTypes(isExpert);
            return Ok(result);
        }

        [HttpPost("CreatePost"), Authorize(Roles = "Cat Owner,Cat Expert")]
        public async Task<IActionResult> CreatePost([FromBody]PostRequestDTO createPostDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string authorizationHeader = Request.Headers["Authorization"]!;
            string token = authorizationHeader.Substring("Bearer ".Length);

            var userResult = await _authService.GetUserFromJWTToken(token);

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

        [HttpPut("EditPost/{Id}"), Authorize(Roles = "Cat Owner,Cat Expert")]
        public async Task<IActionResult> EditPost(long Id, [FromBody] EditPostRequestDTO editPostRequestDTO)
        {
            string authorizationHeader = Request.Headers["Authorization"]!;
            string token = authorizationHeader.Substring("Bearer ".Length);

            var userResult = await _authService.GetUserFromJWTToken(token);

            if (!userResult.IsSuccessful)
            {
                return Unauthorized("invalid token");
            }

            var checkPostRes = _postService.CheckIfPostExist(userResult.Result!.Id, Id);

            if (!checkPostRes.IsSuccessful)
            {
                return BadRequest(checkPostRes.ErrorMessage);
            }

            var postActRes = await _postService.EditPost(userResult.Result!.Id, editPostRequestDTO);

            if (!postActRes.IsSuccessful)
            {
                return BadRequest("fail to act the post");
            }

            return Ok();
        }

        [HttpDelete("DeletePost/{Id}"), Authorize(Roles = "Cat Owner,Cat Expert")]
        public async Task<IActionResult> DeleteCat(long Id)
        {
            string authorizationHeader = Request.Headers["Authorization"]!;
            string token = authorizationHeader.Substring("Bearer ".Length);

            var userResult = await _authService.GetUserFromJWTToken(token);

            if (!userResult.IsSuccessful)
            {
                return Unauthorized("invalid token");
            }

            var checkPostRes = _postService.CheckIfPostExist(userResult.Result!.Id, Id);

            if (!checkPostRes.IsSuccessful)
            {
                return BadRequest(checkPostRes.ErrorMessage);
            }

            var catRes = await _postService.DeletePost(Id);

            if (!catRes.IsSuccessful)
            {
                return BadRequest(catRes.ErrorMessage);
            }

            return Ok();
        }
    }
}
