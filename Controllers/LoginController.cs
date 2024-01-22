using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using BookshelfApi.Models;
using System.Text;

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
                    DateTime expDate = DateTime.Now.AddMinutes(5);
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("My@Secret@Key@5000"));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    var tokenOptions = new JwtSecurityToken(
                        issuer: "https://localhost:5001",
                        audience: "https://localhost:5001",
                        expires: expDate,
                        signingCredentials: signinCredentials
                    );
                    var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                    return Ok(new AuthToken() { Token = token, ExpDate = expDate});
                }

                return Unauthorized();
            }

            return BadRequest();
        }
    }
}