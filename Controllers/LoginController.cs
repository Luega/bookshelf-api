using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using BookshelfApi.Models;
using System.Text;
using BookshelfApi.Interfaces;

namespace BookshelfApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginCredentials loginCredentials) 
        {
            if (loginCredentials != null)
            {
                if (_loginService.IsAuthenticate(loginCredentials))
                {
                    return Ok(_loginService.CreateToken());
                }

                return Unauthorized();
            }

            return Unauthorized();
        }
    }
}