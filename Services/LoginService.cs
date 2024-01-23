using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookshelfApi.Interfaces;
using BookshelfApi.Models;
using Microsoft.IdentityModel.Tokens;

namespace BookshelfApi.Services
{
    public class LoginService : ILoginService
    {
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
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("My@Secret@Key@5000"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: "https://localhost:5001",
                audience: "https://localhost:5001",
                expires: expDate,
                signingCredentials: signinCredentials
            );
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                
            return new AuthToken() { Token = token, ExpDate = expDate };
        }
    }
}