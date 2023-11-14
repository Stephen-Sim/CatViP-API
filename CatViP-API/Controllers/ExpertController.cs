using CatViP_API.DTOs.ExpertDTOs;
using CatViP_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;

namespace CatViP_API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAll")]
    [ApiController]
    public class ExpertController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IExpertService _expertService;
        public ExpertController(IAuthService authService, IExpertService expertService)
        {
            _authService = authService;
            _expertService = expertService;
        }

        [HttpGet("GetApplications"), Authorize(Roles = "Cat Owner,Cat Expert")]
        public async Task<IActionResult> GetApplications() 
        {
            string authorizationHeader = Request.Headers["Authorization"]!;
            string token = authorizationHeader.Substring("Bearer ".Length);

            var userResult = await _authService.GetUserFromJWTToken(token);

            if (!userResult.IsSuccessful)
            {
                return Unauthorized("invalid token");
            }

            var applications = _expertService.GetApplications(userResult.Result!.Id);

            return Ok(applications);
        }

        [HttpPost("ApplyAsExpert"), Authorize(Roles = "Cat Owner")]
        public async Task<IActionResult> ExpertApplicationAsync([FromBody] ExpertApplicationRequestDTO expertApplicationRequestDTO)
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

            var applicationRes = await _expertService.ApplyAsExpert(userResult.Result!.Id, expertApplicationRequestDTO);

            if (!applicationRes.IsSuccessful)
            {
                return BadRequest(applicationRes.ErrorMessage);
            }

            return Ok();
        }

        [HttpGet("GetPendingApplications"), Authorize(Roles = "System Admin")]
        public async Task<IActionResult> GetPendingApplications()
        {
            string authorizationHeader = Request.Headers["Authorization"]!;
            string token = authorizationHeader.Substring("Bearer ".Length);

            var userResult = await _authService.GetUserFromJWTToken(token);

            if (!userResult.IsSuccessful)
            {
                return Unauthorized("invalid token");
            }

            var applications = _expertService.GetPendingApplications();

            return Ok(applications);
        }

        [HttpPut("UpateExpertApplicationStatus"), Authorize(Roles = "System Admin")]
        public async Task<IActionResult> UpateExpertApplicationStatusAsync(ExpertApplicationActionRequestDTO expertApplicationActionRequestDTO)
        {
            string authorizationHeader = Request.Headers["Authorization"]!;
            string token = authorizationHeader.Substring("Bearer ".Length);

            var userResult = await _authService.GetUserFromJWTToken(token);

            if (!userResult.IsSuccessful)
            {
                return Unauthorized("invalid token");
            }

            var checkApplicationRes = _expertService.CheckIfPendingApplicationExist(expertApplicationActionRequestDTO.ApplictionId);

            if (!checkApplicationRes.IsSuccessful)
            {
                return BadRequest(checkApplicationRes.ErrorMessage);
            }

            var updateStatusRes = await _expertService.UpdateApplicationStatus(expertApplicationActionRequestDTO);

            if (!updateStatusRes.IsSuccessful)
            {
                return BadRequest(updateStatusRes.ErrorMessage);
            }

            return Ok();
        }
    }
}
