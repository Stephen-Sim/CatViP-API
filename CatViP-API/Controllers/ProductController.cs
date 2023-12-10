using CatViP_API.DTOs.ProductDTOs;
using CatViP_API.Services;
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
    public class ProductController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IProductService _productService;

        public ProductController(IAuthService authService, IProductService productService)
        {
            this._authService = authService;
            _productService = productService;
        }

        [HttpGet("GetProductTypes"), Authorize(Roles = "Cat Product Seller")]
        public async Task<IActionResult> GetProductTypes()
        {
            string authorizationHeader = Request.Headers["Authorization"]!;
            string token = authorizationHeader.Substring("Bearer ".Length);

            var userResult = await _authService.GetUserFromJWTToken(token);

            if (!userResult.IsSuccessful)
            {
                return Unauthorized("invalid token");
            }

            var productTypes = _productService.GetProductTypes();

            return Ok(productTypes);
        }

        [HttpPost("StoreProduct"), Authorize(Roles = "Cat Product Seller")]
        public async Task<IActionResult> StoreProduct(ProductRequestDTO productRequestDTO)
        {
            string authorizationHeader = Request.Headers["Authorization"]!;
            string token = authorizationHeader.Substring("Bearer ".Length);

            var userResult = await _authService.GetUserFromJWTToken(token);

            if (!userResult.IsSuccessful)
            {
                return Unauthorized("invalid token");
            }

            var productTypes = _productService.StoreProduct(userResult.Result!.Id, productRequestDTO);

            return Ok(productTypes);
        }
    }
}
