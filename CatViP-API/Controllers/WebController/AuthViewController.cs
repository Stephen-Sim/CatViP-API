using Microsoft.AspNetCore.Mvc;

namespace CatViP_API.Controllers.WebController
{
    [Route("auth")]
    public class AuthViewController : Controller
    {
        [HttpGet("forgot-password")]
        public IActionResult Index(string email)
        {
            return View("index", email);
        }
    }
}
