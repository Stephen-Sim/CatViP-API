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
    public class AnalysisController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IAnalysisService _analysisService;

        public AnalysisController(IAuthService authService, IAnalysisService analysisService)
        {
            _authService = authService;
            _analysisService = analysisService;
        }

        [HttpGet("GetExpertTipsCount"), Authorize(Roles = "System Admin")]
        public async Task<IActionResult> GetExpertTipsCount([FromQuery]string query)
        {
            string authorizationHeader = Request.Headers["Authorization"]!;
            string token = authorizationHeader.Substring("Bearer ".Length);

            var userResult = await _authService.GetUserFromJWTToken(token);

            if (!userResult.IsSuccessful)
            {
                return Unauthorized("invalid token");
            }

            var countRes = _analysisService.GetExpertTipsCount(query);

            if (!countRes.IsSuccessful)
            {
                return BadRequest(countRes.ErrorMessage);
            }

            return Ok(countRes.Result!);
        }

        [HttpGet("GetUsersCount"), Authorize(Roles = "System Admin")]
        public async Task<IActionResult> GetUsersCount()
        {
            string authorizationHeader = Request.Headers["Authorization"]!;
            string token = authorizationHeader.Substring("Bearer ".Length);

            var userResult = await _authService.GetUserFromJWTToken(token);

            if (!userResult.IsSuccessful)
            {
                return Unauthorized("invalid token");
            }

            var usersCount = _analysisService.GetUsersCount();

            return Ok(usersCount);
        }

        [HttpGet("GetMissingCatsCount"), Authorize(Roles = "System Admin")]
        public async Task<IActionResult> GetMissingCatsCount([FromQuery] string query)
        {
            string authorizationHeader = Request.Headers["Authorization"]!;
            string token = authorizationHeader.Substring("Bearer ".Length);

            var userResult = await _authService.GetUserFromJWTToken(token);

            if (!userResult.IsSuccessful)
            {
                return Unauthorized("invalid token");
            }

            var countRes = _analysisService.GetMissingCatsCount(query);

            if (!countRes.IsSuccessful)
            {
                return BadRequest(countRes.ErrorMessage);
            }

            return Ok(countRes.Result!);
        }
    }
}
