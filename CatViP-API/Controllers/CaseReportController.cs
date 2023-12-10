using CatViP_API.DTOs.CaseReportDTOs;
using CatViP_API.Services;
using CatViP_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatViP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaseReportController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ICaseReportService _caseReportService;

        public CaseReportController(IAuthService authService, ICaseReportService caseReportService)
        {
            _authService = authService;
            _caseReportService = caseReportService;

        }

        [HttpPost("CreateCaseReport"), Authorize(Roles = "Cat Owner,Cat Expert")]
        public async Task<IActionResult> CreateCaseReport([FromBody] CaseReportRequestDTO caseReportRequestDTO)
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

            var createPostResult = await _caseReportService.CreateCaseReport(userResult.Result!.Id, caseReportRequestDTO);

            if (!createPostResult.IsSuccessful)
            {
                return BadRequest(createPostResult.ErrorMessage);
            }

            return Ok();
        }

    }
}
