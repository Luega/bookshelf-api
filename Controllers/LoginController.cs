using BookshelfApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookshelfApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private static readonly List<LoginCredentials> loginCredentialsList = new()
        {
            new() { Username = "admin", Password = "admin"},
        };

        [HttpPost]
        public IActionResult Login([FromBody] LoginCredentials loginCredentials) 
        {
            if (loginCredentials != null)
            {
                var existingLoginCredentials = loginCredentialsList.FirstOrDefault(c =>
                    c.Username == loginCredentials.Username && c.Password == loginCredentials.Password);
                
                if (existingLoginCredentials != null)
                {
                    return Ok();
                }

                return Unauthorized();
            }

            return BadRequest();
        }
    }
}