using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class JwtTokenGenerator
    {
        private readonly IConfiguration _config;

        public JwtTokenGenerator(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(string email, string nombre, string dni)
        {
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                       {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Name, nombre),
                new Claim("Dni", dni)
            }),
                Expires = DateTime.UtcNow.AddHours(6),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(
                           new SymmetricSecurityKey(key),
                           SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}
