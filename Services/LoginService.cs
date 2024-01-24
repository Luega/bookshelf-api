using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BookshelfApi.Interfaces;
using BookshelfApi.Models;
using Microsoft.IdentityModel.Tokens;

namespace BookshelfApi.Services
{
    public class LoginService : ILoginService
    {
        private readonly IConfiguration _config;

        public LoginService(IConfiguration config) 
        {
            _config = config;
        }

        private static readonly List<LoginCredentials> loginCredentialsList = new()
        {
            new() { Username = "admin", Password = "admin"},
        };

        public bool IsAuthenticate(LoginCredentials loginCredentials)
        {
            var existingLoginCredentials = loginCredentialsList.FirstOrDefault(c =>
                    c.Username == loginCredentials.Username && c.Password == loginCredentials.Password);
                
            if (existingLoginCredentials != null)
            {
                return true;
            }

            return false;
        }

        public AuthToken CreateToken()
        {
            DateTime expDate = DateTime.Now.AddMinutes(5);
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["SecretTokenKey"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: _config["TokenIssuer"],
                audience: "https://localhost:5001",
                expires: expDate,
                signingCredentials: signinCredentials
            );
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                
            return new AuthToken() { Token = token, ExpDate = expDate };
        }
    }
}