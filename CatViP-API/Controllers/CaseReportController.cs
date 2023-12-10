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

        [HttpGet("GetOwnPendingCaseReports"), Authorize(Roles = "Cat Owner,Cat Expert")]
        public async Task<IActionResult> GetOwnCaseReports()
        {
            string authorizationHeader = Request.Headers["Authorization"]!;
            string token = authorizationHeader.Substring("Bearer ".Length);

            var userResult = await _authService.GetUserFromJWTToken(token);

            if (!userResult.IsSuccessful)
            {
                return Unauthorized("invalid token");
            }

            var cases = _caseReportService.GetOwnCaseReports(userResult.Result!.Id);

            return Ok(cases);
        }

        [HttpPut("SettleCaseReport/{Id}"), Authorize(Roles = "Cat Owner,Cat Expert")]
        public async Task<IActionResult> SettleCaseReport(long Id)
        {
            string authorizationHeader = Request.Headers["Authorization"]!;
            string token = authorizationHeader.Substring("Bearer ".Length);

            var userResult = await _authService.GetUserFromJWTToken(token);

            if (!userResult.IsSuccessful)
            {
                return Unauthorized("invalid token");
            }

            var checkIsReportExistRes = _caseReportService.CheckIsReportExist(userResult.Result!.Id, Id);

            if (!checkIsReportExistRes.IsSuccessful)
            {
                return BadRequest(checkIsReportExistRes.ErrorMessage);
            }

            var res = await _caseReportService.SettleCaseReport(Id);

            if (!res.IsSuccessful)
            {
                return BadRequest(res.ErrorMessage);
            }

            return Ok();
        }

        [HttpDelete("RevokeCaseReport/{Id}"), Authorize(Roles = "Cat Owner,Cat Expert")]
        public async Task<IActionResult> RevokeCaseReport(long Id)
        {
            string authorizationHeader = Request.Headers["Authorization"]!;
            string token = authorizationHeader.Substring("Bearer ".Length);

            var userResult = await _authService.GetUserFromJWTToken(token);

            if (!userResult.IsSuccessful)
            {
                return Unauthorized("invalid token");
            }

            var checkIsReportExistRes = _caseReportService.CheckIsReportExist(userResult.Result!.Id, Id);

            if (!checkIsReportExistRes.IsSuccessful)
            {
                return BadRequest(checkIsReportExistRes.ErrorMessage);
            }

            var res = await _caseReportService.RevokeCaseReport(Id);

            if (!res.IsSuccessful)
            {
                return BadRequest(res.ErrorMessage);
            }

            return Ok();
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
