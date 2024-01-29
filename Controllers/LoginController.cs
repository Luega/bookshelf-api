using Microsoft.AspNetCore.Mvc;
using BookshelfApi.Models;
using BookshelfApi.Interfaces;

namespace BookshelfApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILoginService loginService, ILogger<LoginController> logger)
        {
            _loginService = loginService;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginCredentials loginCredentials) 
        {
            if (loginCredentials == null)
                return Unauthorized();

            try
            {
                if (_loginService.IsAuthenticate(loginCredentials))
                {
                    return Ok(_loginService.CreateToken());
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in Login.");
                return StatusCode(500, "Internal server error: " + ex.Message);
            }    
        }
    }
}