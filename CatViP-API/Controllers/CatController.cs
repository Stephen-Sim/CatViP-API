using CatViP_API.DTOs.CatDTOs;
using CatViP_API.Models;
using CatViP_API.Services;
using CatViP_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatViP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ICatService _catService;

        public CatController(IAuthService authService, ICatService catService)
        {
            this._authService = authService;
            _catService = catService;
        }

        [HttpGet("GetCats"), Authorize(Roles = "Cat Owner,Cat Expert")]
        public async Task<IActionResult> GetCats()
        {
            string authorizationHeader = Request.Headers["Authorization"]!;
            string token = authorizationHeader.Substring("Bearer ".Length);

            var userResult = await _authService.GetUserFromJWTToken(token);

            if (!userResult.IsSuccessful)
            {
                return Unauthorized("invalid token");
            }

            var cats = _catService.GetCats(userResult.Result!.Id);

            return Ok(cats);
        }

        [HttpGet("GetCat"), Authorize(Roles = "Cat Owner,Cat Expert")]
        public async Task<IActionResult> GetCat([FromQuery] long Id)
        {
            string authorizationHeader = Request.Headers["Authorization"]!;
            string token = authorizationHeader.Substring("Bearer ".Length);

            var userResult = await _authService.GetUserFromJWTToken(token);

            if (!userResult.IsSuccessful)
            {
                return Unauthorized("invalid token");
            }

            var checkCatRes = _catService.CheckIfCatExist(Id);

            if (!checkCatRes.IsSuccessful)
            {
                return BadRequest(checkCatRes.ErrorMessage);
            }

            var cats = _catService.GetCat(Id);

            return Ok(cats);
        }

        [HttpPost("StoreCat"), Authorize(Roles = "Cat Owner,Cat Expert")]
        public async Task<IActionResult> StoreCat([FromBody] CreateCatRequestDTO createCatRequestDTO)
        {
            string authorizationHeader = Request.Headers["Authorization"]!;
            string token = authorizationHeader.Substring("Bearer ".Length);

            var userResult = await _authService.GetUserFromJWTToken(token);

            if (!userResult.IsSuccessful)
            {
                return Unauthorized("invalid token");
            }

            var catRes = await _catService.StoreCat(userResult.Result!.Id, createCatRequestDTO);

            if (!catRes.IsSuccessful)
            {
                return BadRequest(catRes.ErrorMessage);
            }

            return Ok();
        }

        [HttpPut("EditCat"), Authorize(Roles = "Cat Owner,Cat Expert")]
        public async Task<IActionResult> EditCat([FromBody] EditCatRequestDTO editCatRequestDTO)
        {
            string authorizationHeader = Request.Headers["Authorization"]!;
            string token = authorizationHeader.Substring("Bearer ".Length);

            var userResult = await _authService.GetUserFromJWTToken(token);

            if (!userResult.IsSuccessful)
            {
                return Unauthorized("invalid token");
            }

            var checkCatRes = _catService.CheckIfCatExist(editCatRequestDTO.Id);

            if (!checkCatRes.IsSuccessful)
            {
                return BadRequest(checkCatRes.ErrorMessage);
            }

            var catRes = await _catService.EditCat(editCatRequestDTO);

            if (!catRes.IsSuccessful)
            {
                return BadRequest(catRes.ErrorMessage);
            }

            return Ok();
        }

        [HttpDelete("DeleteCat"), Authorize(Roles = "Cat Owner,Cat Expert")]
        public async Task<IActionResult> DeleteCat([FromForm] long Id)
        {
            string authorizationHeader = Request.Headers["Authorization"]!;
            string token = authorizationHeader.Substring("Bearer ".Length);

            var userResult = await _authService.GetUserFromJWTToken(token);

            if (!userResult.IsSuccessful)
            {
                return Unauthorized("invalid token");
            }

            var checkCatRes = _catService.CheckIfCatExist(Id);

            if (!checkCatRes.IsSuccessful)
            {
                return BadRequest(checkCatRes.ErrorMessage);
            }

            var catRes = await _catService.DeleteCat(Id);

            if (!catRes.IsSuccessful)
            {
                return BadRequest(catRes.ErrorMessage);
            }

            return Ok();
        }
    }
}
